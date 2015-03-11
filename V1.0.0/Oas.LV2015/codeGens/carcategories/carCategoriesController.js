'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'carCategoriesService', 'modalService'];

    var CarCategoriesController = function ($scope, $filter,authService, $location, $window,
        $timeout, carCategoriesService, modalService) {

        var vm = this;
        vm.carCategories = [];
        vm.filteredCarCategories = [];
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
            filtercarCategories(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCarCategories();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcarCategories();
        }


        function filterCarCategories(filterText) {
            
            vm.filteredCarCategories = $filter("carCategoryFilter")(vm.carCategories, filterText);
            vm.filteredCount = vm.filteredCarCategories.length;
        }

		vm.deleteCarCategory = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var carCategory = getCarCategoryById(id);
            var carCategoryName = CarCategory;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete CarCategory',
                headerText: 'Delete ' + carCategoryName + '?',
                bodyText: 'Are you sure you want to delete this '+carCategoryName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carCategoriesService.deleteCarCategory(id).then(function () {
                        for (var i = 0; i < vm.carCategories.length; i++) {
                            if (vm.carCategories[i].id === id) {
                                vm.carCategories.splice(i, 1);
                                break;
                            }
							if (vm.filteredCarCategories[i].id === id) {
                                vm.filteredCarCategories.splice(i, 1);
                                break;
                            }
                        }
                        filterCarCategories(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting carCategory: ' + error.message);
                    });
                }
            });
        };

		function getCarCategoriesById(id) {
            for (var i = 0; i < vm.carCategories.length; i++) {
                var carCategory = vm.carCategories[i];
                if (carCategory.id === id) {
                    return carCategory;
                }
            }
            return null;
        }

        function getCarCategories() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            carCategoriesService.searchCarCategory(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.carCategories = response.data;
                filterCarCategories(vm.searchText);
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

    CarCategoriesController.$inject = injectParams;

    app.register.controller('CarCategoriesController', CarCategoriesController);

});
