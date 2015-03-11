'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window', '$routeParams',
                        '$timeout', 'carsService', 'carModelsService', 'modalService'];

    var carItemController = function ($scope, $location, $window, $routeParams,
        $timeout, carsService, carModelsService, modalService) {

        var vm = this,
                    carId = $routeParams.carId,
                    timer,
                    onRouteChangeOff;
        vm.car = {};
        vm.carId = carId;
        vm.carModels = {};
        vm.title = (carId == '0') ? 'Thêm' : 'Chỉnh sửa';
        vm.buttonText = (carId == '0') ? 'Thêm' : 'Sửa';
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.Save = function () {
            if (carId != '0') {
                carsService.updateCar(vm.car).then(function () {
                    alert('Cập nhập thành công');

                    $location.path('/cars');

                }, processError);
            }
            else {
                carsService.addCar(vm.car).then(function () {
                    alert('Thêm mới thành công');

                    $location.path('/cars');

                }, processError);
            }
        }

        function initialData() {
            if (carId != '0') {
                carsService.getCar(carId).then(function (data) {
                    vm.car = data;
                }, processError);
            }
            getCarModels();
        }

        function getCarModels() {
            carModelsService.getCarModels().then(function (data) {
                vm.carModels = data;
            }, processError);
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

        initialData();

    };

    carItemController.$inject = injectParams;

    app.register.controller('carItemController', carItemController);
});