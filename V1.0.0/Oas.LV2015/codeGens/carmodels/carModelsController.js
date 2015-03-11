'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'carModelsService', 'modalService'];

    var CarModelsController = function ($scope, $filter,authService, $location, $window,
        $timeout, carModelsService, modalService) {

        var vm = this;
        vm.carModels = [];
        vm.filteredCarModels = [];
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
            filtercarModels(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCarModels();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcarModels();
        }


        function filterCarModels(filterText) {
            
            vm.filteredCarModels = $filter("carModelFilter")(vm.carModels, filterText);
            vm.filteredCount = vm.filteredCarModels.length;
        }

		vm.deleteCarModel = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var carModel = getCarModelById(id);
            var carModelName = CarModel;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete CarModel',
                headerText: 'Delete ' + carModelName + '?',
                bodyText: 'Are you sure you want to delete this '+carModelName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carModelsService.deleteCarModel(id).then(function () {
                        for (var i = 0; i < vm.carModels.length; i++) {
                            if (vm.carModels[i].id === id) {
                                vm.carModels.splice(i, 1);
                                break;
                            }
							if (vm.filteredCarModels[i].id === id) {
                                vm.filteredCarModels.splice(i, 1);
                                break;
                            }
                        }
                        filterCarModels(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting carModel: ' + error.message);
                    });
                }
            });
        };

		function getCarModelsById(id) {
            for (var i = 0; i < vm.carModels.length; i++) {
                var carModel = vm.carModels[i];
                if (carModel.id === id) {
                    return carModel;
                }
            }
            return null;
        }

        function getCarModels() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            carModelsService.searchCarModel(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.carModels = response.data;
                filterCarModels(vm.searchText);
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

    CarModelsController.$inject = injectParams;

    app.register.controller('CarModelsController', CarModelsController);

});
