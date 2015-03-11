'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$routeParams',
                        '$timeout', 'config', 'businessPromotionsService', 'modalService'];

    var BusinessPromotionItemController = function ($scope, $location, $routeParams,
                                           $timeout, config, businessPromotionsService, modalService) {

        var vm = this,
            businessPromotionId = ($routeParams.businessPromotionId) ? parseInt($routeParams.businessPromotionId) : 0,
            timer,
            onRouteChangeOff;

        vm.businessPromotion = {};
        vm.title = (businessPromotionId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.buttonText = (businessPromotionId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.updateStatus = false;
        vm.errorMessage = '';


        vm.saveBusinessPromotion = function () {
            if ($scope.itemForm.$valid) {
                if (!vm.businessPromotion.id) {
                    businessPromotionService.insertBusinessPromotion(vm.businessPromotion).then(processSuccess, processError);
                }
                else {
                    businessPromotionService.updateBusinessPromotion(vm.businessPromotion).then(processSuccess, processError);
                }
            }
        };

        vm.deleteBusinessPromotion = function () {
            var headerText = businessPromotion;
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete BusinessPromotion',
                headerText: 'Delete ' + headerText + '?',
                bodyText: 'Are you sure you want to delete this businessPromotion?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    businessPromotionService.deleteBusinessPromotion(vm.businessPromotion.id).then(function () {
                        onRouteChangeOff(); //Stop listening for location changes
                        $location.path('/businessPromotions');
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

    BusinessPromotionItemController.$inject = injectParams;

    app.register.controller('BusinessPromotionItemController', BusinessPromotionItemController);

});
