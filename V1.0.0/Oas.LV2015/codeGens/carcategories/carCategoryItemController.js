'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$routeParams',
                        '$timeout', 'config', 'carCategoriesService', 'modalService'];

    var CarCategoryItemController = function ($scope, $location, $routeParams,
                                           $timeout, config, carCategoriesService, modalService) {

        var vm = this,
            carCategoryId = ($routeParams.carCategoryId) ? parseInt($routeParams.carCategoryId) : 0,
            timer,
            onRouteChangeOff;

        vm.carCategory = {};
        vm.title = (carCategoryId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.buttonText = (carCategoryId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.updateStatus = false;
        vm.errorMessage = '';


        vm.saveCarCategory = function () {
            if ($scope.itemForm.$valid) {
                if (!vm.carCategory.id) {
                    carCategoryService.insertCarCategory(vm.carCategory).then(processSuccess, processError);
                }
                else {
                    carCategoryService.updateCarCategory(vm.carCategory).then(processSuccess, processError);
                }
            }
        };

        vm.deleteCarCategory = function () {
            var headerText = carCategory;
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete CarCategory',
                headerText: 'Delete ' + headerText + '?',
                bodyText: 'Are you sure you want to delete this carCategory?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carCategoryService.deleteCarCategory(vm.carCategory.id).then(function () {
                        onRouteChangeOff(); //Stop listening for location changes
                        $location.path('/carCategorys');
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

    CarCategoryItemController.$inject = injectParams;

    app.register.controller('CarCategoryItemController', CarCategoryItemController);

});
