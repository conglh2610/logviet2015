'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window', '$routeParams',
                        '$timeout', 'carItemsService', 'carsService', 'modalService'];

    var carItemItemController = function ($scope, $location, $window, $routeParams,
        $timeout, carItemsService, carsService, modalService) {

        var vm = this,
                    carItemId = $routeParams.carItemId,
                    timer,
                    onRouteChangeOff;
        vm.carItem = {};
        vm.carItemId = carItemId;
        vm.cars = {};
        vm.title = (carItemId == '0') ? 'Thêm' : 'Chỉnh sửa';
        vm.buttonText = (carItemId == '0') ? 'Thêm' : 'Sửa';
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.Save = function () {
            if (carItemId != '0') {
                carItemsService.updateCarItem(vm.carItem).then(function () {
                    alert('Cập nhập thành công');

                    $location.path('/carItems');

                }, processError);
            }
            else {
                carItemsService.addCarItem(vm.carItem).then(function () {
                    alert('Thêm mới thành công');

                    $location.path('/carItems');

                }, processError);
            }
        }

        function initialData() {
            if (carItemId != '0') {
                debugger;
                carItemsService.getCarItem(carItemId).then(function (data) {
                    vm.carItem = data;
                }, processError);
            }
            getCars();
        }

        function getCars() {
            debugger;
            carsService.getCars().then(function (data) {
                vm.cars = data;
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

    carItemItemController.$inject = injectParams;

    app.register.controller('carItemItemController', carItemItemController);
});