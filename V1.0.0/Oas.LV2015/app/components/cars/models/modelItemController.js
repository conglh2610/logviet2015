'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window', '$routeParams',
                        '$timeout', 'carModelsService', 'carCategoriesService', 'modalService'];

    var modelItemController = function ($scope, $location, $window, $routeParams,
        $timeout, carModelsService, carCategoriesService, modalService) {

        var vm = this,
                    carModelId = $routeParams.carModelId,
                    timer,
                    onRouteChangeOff;
        vm.carModel = {};
        vm.carCategories = {};
        vm.carModelId = carModelId;
        vm.title = (carModelId == '0') ? 'Thêm' : 'Chỉnh sửa';
        vm.buttonText = (carModelId == '0') ? 'Thêm' : 'Sửa';
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.Save = function () {
            if (carModelId != '0') {
                carModelsService.updateCarModel(vm.carModel).then(function () {
                    alert('Cập nhập thành công');

                    $location.path('/models');

                }, processError);
            }
            else {
                carModelsService.addCarModel(vm.carModel).then(function () {
                    alert('Thêm mới thành công');

                    $location.path('/models');

                }, processError);
            }
        }

        function initialData() {
            if (carModelId != '0') {
                carModelsService.getCarModel(carModelId).then(function (data) {
                    vm.carModel = data;
                }, processError);
            }

            getCarCategories();
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

        function getCarCategories() {
            carCategoriesService.getCarCategories().then(function (data) {
                vm.carCategories = data;
            }, $scope.processError);
        }

        initialData();

    };


    modelItemController.$inject = injectParams;

    app.register.controller('modelItemController', modelItemController);
});