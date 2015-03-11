'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window', '$routeParams',
                        '$timeout', 'driversService', 'modalService'];

    var DriverItemController = function ($scope, $location, $window, $routeParams,
        $timeout, driversService, modalService) {

        var vm = this,
                    driverId = $routeParams.driverId,
                    timer,
                    onRouteChangeOff;
        vm.driver = {};
        vm.states = [];
        vm.title = (driverId == '0') ? 'Add' : 'Edit';
        vm.buttonText = (driverId == '0') ? 'Add' : 'Update';
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.Save = function () {
            if (driverId != '0') {
                driversService.updateDriver(vm.driver).then(function () {
                    alert('Cập nhập thành công');

                    $location.path('/drivers');

                }, processError);
            }
            else {
                driversService.addDriver(vm.driver).then(function () {
                    alert('Thêm mới thành công');

                    $location.path('/drivers');

                }, processError);
            }
        }

        function init() {
            if (driverId != '0') {
                driversService.getDriver(driverId).then(function (driver) {
                    vm.driver = driver;
                }, processError);
            }
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

        init();

    };



    DriverItemController.$inject = injectParams;

    app.register.controller('driverItemController', DriverItemController);
});