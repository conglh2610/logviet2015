'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'businessesService', 'modalService'];

    var BusinessesController = function ($scope, $filter,authService, $location, $window,
        $timeout, businessesService, modalService) {

        var vm = this;
        vm.businesses = [];
        vm.filteredBusinesses = [];
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
            filterbusinesses(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getBusinesses();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getbusinesses();
        }


        function filterBusinesses(filterText) {
            
            vm.filteredBusinesses = $filter("businessFilter")(vm.businesses, filterText);
            vm.filteredCount = vm.filteredBusinesses.length;
        }

		vm.deleteBusiness = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var business = getBusinessById(id);
            var businessName = Business;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Business',
                headerText: 'Delete ' + businessName + '?',
                bodyText: 'Are you sure you want to delete this '+businessName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    businessesService.deleteBusiness(id).then(function () {
                        for (var i = 0; i < vm.businesses.length; i++) {
                            if (vm.businesses[i].id === id) {
                                vm.businesses.splice(i, 1);
                                break;
                            }
							if (vm.filteredBusinesses[i].id === id) {
                                vm.filteredBusinesses.splice(i, 1);
                                break;
                            }
                        }
                        filterBusinesses(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting business: ' + error.message);
                    });
                }
            });
        };

		function getBusinessesById(id) {
            for (var i = 0; i < vm.businesses.length; i++) {
                var business = vm.businesses[i];
                if (business.id === id) {
                    return business;
                }
            }
            return null;
        }

        function getBusinesses() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            businessesService.searchBusiness(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.businesses = response.data;
                filterBusinesses(vm.searchText);
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

    BusinessesController.$inject = injectParams;

    app.register.controller('BusinessesController', BusinessesController);

});
