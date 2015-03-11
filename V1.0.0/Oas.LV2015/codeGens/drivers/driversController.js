'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'driversService', 'modalService'];

    var DriversController = function ($scope, $filter,authService, $location, $window,
        $timeout, driversService, modalService) {

        var vm = this;
        vm.drivers = [];
        vm.filteredDrivers = [];
        vm.filteredCount = 0;
        vm.currentPage = 1;
        vm.itemPerPage = 10;
        vm.totalRecords = 0;
        vm.errorMessage = "";
        vm.sortDirection = "asc";
        vm.columnName = "name";
        vm.itemPerPage = 5;

        vm.searchText = "";
        vm.cardAnimationClass = '.card-animation';
        vm.DisplayModeEnum = {            
            List: 0,
            Card: 1
        };


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

        function processError(error) {
            vm.errorMessage = error.message;
            alert(error);
        };

        /*region ==> Get Data*/

        vm.searchTextChanged = function () {
            filterdrivers(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getDrivers();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getdrivers();
        }


        function filterDrivers(filterText) {
            
            vm.filteredDrivers = $filter("driverFilter")(vm.drivers, filterText);
            vm.filteredCount = vm.filteredDrivers.length;
        }

		vm.deleteDriver = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var driver = getDriverById(id);
            var driverName = Driver;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Driver',
                headerText: 'Delete ' + driverName + '?',
                bodyText: 'Are you sure you want to delete this '+driverName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    driversService.deleteDriver(id).then(function () {
                        for (var i = 0; i < vm.drivers.length; i++) {
                            if (vm.drivers[i].id === id) {
                                vm.drivers.splice(i, 1);
                                break;
                            }
							if (vm.filteredDrivers[i].id === id) {
                                vm.filteredDrivers.splice(i, 1);
                                break;
                            }
                        }
                        filterDrivers(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting driver: ' + error.message);
                    });
                }
            });
        };

		function getDriversById(id) {
            for (var i = 0; i < vm.drivers.length; i++) {
                var driver = vm.drivers[i];
                if (driver.id === id) {
                    return driver;
                }
            }
            return null;
        }

        function getDrivers() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            driversService.searchDriver(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.drivers = response.data;
                filterDrivers(vm.searchText);
            });

            
        }
        /*END*/


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

    app.register.controller('DriversController', DriversController);

});
