'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window',
                        '$timeout', 'driversService', 'modalService'];

    var DriversController = function ($scope, $location, $window,
        $timeout, driversService, modalService) {

        var vm = this;
        vm.drivers = [];
        vm.errorMessage = "";

        //Paging
        vm.currentPage = 1;
        vm.itemPerPage = 10;
        vm.totalRecords = 0;
        vm.columnName = "name";
        vm.sortDirection = false;

        vm.searchText = "";
        vm.cardAnimationClass = '.card-animation';
        vm.DisplayModeEnum = {
            Card: 0,
            List: 1
        };

        vm.Save = function () {
            if ($scope.driverform.$valid) {
                driversService.insertDriver(vm.driver).then(processSuccess, processError);
            }
        };

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getDrivers();
        }

        vm.searchTextChanged = function () {
            vm.currentPage = 1;
            getDrivers();
        }

        vm.setOrder = function (columnName) {
            if (columnName === vm.columnName) {
                vm.sortDirection = !vm.sortDirection;
            }
            vm.columnName = columnName;
            
            vm.currentPage = 1;
            getDrivers();
        }

        vm.deleteDriver = function (id) {
            
            var driver = getDriverById(id);

            var driverName = driver.FirstName + ' ' + driver.LastName;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Driver',
                headerText: 'Delete ' + driverName + '?',
                bodyText: 'Bạn có muốn xóa tài xế này không?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    driversService.deleteDriver(id).then(function () {
                        initalData();
                    }, function (error) {
                        $window.alert('Error deleting driver: ' + error.message);
                    });
                }
            });
        }

        vm.navigate = function (url) {
            $location.path(url);
        };

        function initalData() {
            vm.currentPage = 1;
            getDrivers();
        }



        function processSuccess() {
            $scope.driverform.$dirty = false;

            alert('Save success');
        };

        function processError(error) {
            vm.errorMessage = error.message;

            alert(error);
        };

        /*khoa added*/
        function getDrivers() {
            
            var criteria = {
                CurrentPage: vm.currentPage - 1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection,
                Name: vm.searchText,
            };


            driversService.search(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers.inlinecount);
                vm.drivers = response.data;                
            });



        }

        function getDriverById(id) {

            for (var i = 0; i < vm.drivers.length; i++) {
                var driver = vm.drivers[i];
                if (driver.Id === id) {
                    return driver;
                }
            }
            return null;
        }

        vm.changeDisplayMode = function (displayMode) {
            switch (displayMode) {
                case vm.DisplayModeEnum.Card:
                    vm.listDisplayModeEnabled = false;
                    break;
                case vm.DisplayModeEnum.List:
                    vm.listDisplayModeEnabled = true;
                    break;
            }
        };

        initalData();
    };

    DriversController.$inject = injectParams;

    app.register.controller('driversController', DriversController);
});