'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'packageItemsService', 'modalService'];

    var PackageItemsController = function ($scope, $filter,authService, $location, $window,
        $timeout, packageItemsService, modalService) {

        var vm = this;
        vm.packageItems = [];
        vm.filteredPackageItems = [];
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
            filterpackageItems(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getPackageItems();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getpackageItems();
        }


        function filterPackageItems(filterText) {
            
            vm.filteredPackageItems = $filter("packageItemFilter")(vm.packageItems, filterText);
            vm.filteredCount = vm.filteredPackageItems.length;
        }

		vm.deletePackageItem = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var packageItem = getPackageItemById(id);
            var packageItemName = PackageItem;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete PackageItem',
                headerText: 'Delete ' + packageItemName + '?',
                bodyText: 'Are you sure you want to delete this '+packageItemName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    packageItemsService.deletePackageItem(id).then(function () {
                        for (var i = 0; i < vm.packageItems.length; i++) {
                            if (vm.packageItems[i].id === id) {
                                vm.packageItems.splice(i, 1);
                                break;
                            }
							if (vm.filteredPackageItems[i].id === id) {
                                vm.filteredPackageItems.splice(i, 1);
                                break;
                            }
                        }
                        filterPackageItems(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting packageItem: ' + error.message);
                    });
                }
            });
        };

		function getPackageItemsById(id) {
            for (var i = 0; i < vm.packageItems.length; i++) {
                var packageItem = vm.packageItems[i];
                if (packageItem.id === id) {
                    return packageItem;
                }
            }
            return null;
        }

        function getPackageItems() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            packageItemsService.searchPackageItem(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.packageItems = response.data;
                filterPackageItems(vm.searchText);
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

    PackageItemsController.$inject = injectParams;

    app.register.controller('PackageItemsController', PackageItemsController);

});
