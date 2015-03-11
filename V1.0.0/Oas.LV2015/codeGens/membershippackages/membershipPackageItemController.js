'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$routeParams',
                        '$timeout', 'config', 'membershipPackagesService', 'modalService'];

    var MembershipPackageItemController = function ($scope, $location, $routeParams,
                                           $timeout, config, membershipPackagesService, modalService) {

        var vm = this,
            membershipPackageId = ($routeParams.membershipPackageId) ? parseInt($routeParams.membershipPackageId) : 0,
            timer,
            onRouteChangeOff;

        vm.membershipPackage = {};
        vm.title = (membershipPackageId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.buttonText = (membershipPackageId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.updateStatus = false;
        vm.errorMessage = '';


        vm.saveMembershipPackage = function () {
            if ($scope.itemForm.$valid) {
                if (!vm.membershipPackage.id) {
                    membershipPackageService.insertMembershipPackage(vm.membershipPackage).then(processSuccess, processError);
                }
                else {
                    membershipPackageService.updateMembershipPackage(vm.membershipPackage).then(processSuccess, processError);
                }
            }
        };

        vm.deleteMembershipPackage = function () {
            var headerText = membershipPackage;
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete MembershipPackage',
                headerText: 'Delete ' + headerText + '?',
                bodyText: 'Are you sure you want to delete this membershipPackage?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    membershipPackageService.deleteMembershipPackage(vm.membershipPackage.id).then(function () {
                        onRouteChangeOff(); //Stop listening for location changes
                        $location.path('/membershipPackages');
                    }, processError);
                }
            });
        };

		
        function init() {
			
            //Make sure they're warned if they made a change but didn't save it
            //Call to $on returns a "deregistration" function that can be called to
            //remove the listener (see routeChange() for an example of using it)
            onRouteChangeOff = $scope.$on('$locationChangeStart', routeChange);
        }

        init();

        function routeChange(event, newUrl, oldUrl) {
            //Navigate to newUrl if the form isn't dirty
            if (!vm.itemForm || !vm.itemForm.$dirty) return;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Ignore Changes',
                headerText: 'Unsaved Changes',
                bodyText: 'You have unsaved changes. Leave the page?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    onRouteChangeOff(); //Stop listening for location changes
                    $location.path($location.url(newUrl).hash()); //Go to page they're interested in
                }
            });

            //prevent navigation by default since we'll handle it
            //once the user selects a dialog option
            event.preventDefault();
            return;
        }


        function processSuccess() {
            $scope.itemForm.$dirty = false;
            vm.updateStatus = true;
            vm.title = 'Edit';
            vm.buttonText = 'Update';
            startTimer();
        }

        function processError(error) {
            vm.errorMessage = error.message;
            startTimer();
        }

        function startTimer() {
            timer = $timeout(function () {
                $timeout.cancel(timer);
                vm.errorMessage = '';
                vm.updateStatus = false;
            }, 3000);
        }
    };

    MembershipPackageItemController.$inject = injectParams;

    app.register.controller('MembershipPackageItemController', MembershipPackageItemController);

});
