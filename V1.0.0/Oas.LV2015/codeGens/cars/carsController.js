'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'carsService', 'modalService'];

    var CarsController = function ($scope, $filter,authService, $location, $window,
        $timeout, carsService, modalService) {

        var vm = this;
        vm.cars = [];
        vm.filteredCars = [];
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
            filtercars(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCars();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcars();
        }


        function filterCars(filterText) {
            
            vm.filteredCars = $filter("carFilter")(vm.cars, filterText);
            vm.filteredCount = vm.filteredCars.length;
        }

		vm.deleteCar = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var car = getCarById(id);
            var carName = Car;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Car',
                headerText: 'Delete ' + carName + '?',
                bodyText: 'Are you sure you want to delete this '+carName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carsService.deleteCar(id).then(function () {
                        for (var i = 0; i < vm.cars.length; i++) {
                            if (vm.cars[i].id === id) {
                                vm.cars.splice(i, 1);
                                break;
                            }
							if (vm.filteredCars[i].id === id) {
                                vm.filteredCars.splice(i, 1);
                                break;
                            }
                        }
                        filterCars(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting car: ' + error.message);
                    });
                }
            });
        };

		function getCarsById(id) {
            for (var i = 0; i < vm.cars.length; i++) {
                var car = vm.cars[i];
                if (car.id === id) {
                    return car;
                }
            }
            return null;
        }

        function getCars() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            carsService.searchCar(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.cars = response.data;
                filterCars(vm.searchText);
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

    CarsController.$inject = injectParams;

    app.register.controller('CarsController', CarsController);

});
