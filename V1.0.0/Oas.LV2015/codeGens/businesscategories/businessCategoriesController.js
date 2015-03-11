'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'businessCategoriesService', 'modalService'];

    var BusinessCategoriesController = function ($scope, $filter,authService, $location, $window,
        $timeout, businessCategoriesService, modalService) {

        var vm = this;
        vm.businessCategories = [];
        vm.filteredBusinessCategories = [];
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
            filterbusinessCategories(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getBusinessCategories();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getbusinessCategories();
        }


        function filterBusinessCategories(filterText) {
            
            vm.filteredBusinessCategories = $filter("businessCategoryFilter")(vm.businessCategories, filterText);
            vm.filteredCount = vm.filteredBusinessCategories.length;
        }

		vm.deleteBusinessCategory = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var businessCategory = getBusinessCategoryById(id);
            var businessCategoryName = BusinessCategory;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete BusinessCategory',
                headerText: 'Delete ' + businessCategoryName + '?',
                bodyText: 'Are you sure you want to delete this '+businessCategoryName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    businessCategoriesService.deleteBusinessCategory(id).then(function () {
                        for (var i = 0; i < vm.businessCategories.length; i++) {
                            if (vm.businessCategories[i].id === id) {
                                vm.businessCategories.splice(i, 1);
                                break;
                            }
							if (vm.filteredBusinessCategories[i].id === id) {
                                vm.filteredBusinessCategories.splice(i, 1);
                                break;
                            }
                        }
                        filterBusinessCategories(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting businessCategory: ' + error.message);
                    });
                }
            });
        };

		function getBusinessCategoriesById(id) {
            for (var i = 0; i < vm.businessCategories.length; i++) {
                var businessCategory = vm.businessCategories[i];
                if (businessCategory.id === id) {
                    return businessCategory;
                }
            }
            return null;
        }

        function getBusinessCategories() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            businessCategoriesService.searchBusinessCategory(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.businessCategories = response.data;
                filterBusinessCategories(vm.searchText);
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

    BusinessCategoriesController.$inject = injectParams;

    app.register.controller('BusinessCategoriesController', BusinessCategoriesController);

});
