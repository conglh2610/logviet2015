'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'carItemsService', 'modalService'];

    var CarItemsController = function ($scope, $filter,authService, $location, $window,
        $timeout, carItemsService, modalService) {

        var vm = this;
        vm.carItems = [];
        vm.filteredCarItems = [];
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
            filtercarItems(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCarItems();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcarItems();
        }


        function filterCarItems(filterText) {
            
            vm.filteredCarItems = $filter("carItemFilter")(vm.carItems, filterText);
            vm.filteredCount = vm.filteredCarItems.length;
        }

		vm.deleteCarItem = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var carItem = getCarItemById(id);
            var carItemName = CarItem;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete CarItem',
                headerText: 'Delete ' + carItemName + '?',
                bodyText: 'Are you sure you want to delete this '+carItemName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carItemsService.deleteCarItem(id).then(function () {
                        for (var i = 0; i < vm.carItems.length; i++) {
                            if (vm.carItems[i].id === id) {
                                vm.carItems.splice(i, 1);
                                break;
                            }
							if (vm.filteredCarItems[i].id === id) {
                                vm.filteredCarItems.splice(i, 1);
                                break;
                            }
                        }
                        filterCarItems(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting carItem: ' + error.message);
                    });
                }
            });
        };

		function getCarItemsById(id) {
            for (var i = 0; i < vm.carItems.length; i++) {
                var carItem = vm.carItems[i];
                if (carItem.id === id) {
                    return carItem;
                }
            }
            return null;
        }

        function getCarItems() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            carItemsService.searchCarItem(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.carItems = response.data;
                filterCarItems(vm.searchText);
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

    CarItemsController.$inject = injectParams;

    app.register.controller('CarItemsController', CarItemsController);

});
