'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'carBookingsService', 'modalService'];

    var CarBookingsController = function ($scope, $filter,authService, $location, $window,
        $timeout, carBookingsService, modalService) {

        var vm = this;
        vm.carBookings = [];
        vm.filteredCarBookings = [];
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
            filtercarBookings(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCarBookings();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcarBookings();
        }


        function filterCarBookings(filterText) {
            
            vm.filteredCarBookings = $filter("carBookingFilter")(vm.carBookings, filterText);
            vm.filteredCount = vm.filteredCarBookings.length;
        }

		vm.deleteCarBooking = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var carBooking = getCarBookingById(id);
            var carBookingName = CarBooking;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete CarBooking',
                headerText: 'Delete ' + carBookingName + '?',
                bodyText: 'Are you sure you want to delete this '+carBookingName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carBookingsService.deleteCarBooking(id).then(function () {
                        for (var i = 0; i < vm.carBookings.length; i++) {
                            if (vm.carBookings[i].id === id) {
                                vm.carBookings.splice(i, 1);
                                break;
                            }
							if (vm.filteredCarBookings[i].id === id) {
                                vm.filteredCarBookings.splice(i, 1);
                                break;
                            }
                        }
                        filterCarBookings(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting carBooking: ' + error.message);
                    });
                }
            });
        };

		function getCarBookingsById(id) {
            for (var i = 0; i < vm.carBookings.length; i++) {
                var carBooking = vm.carBookings[i];
                if (carBooking.id === id) {
                    return carBooking;
                }
            }
            return null;
        }

        function getCarBookings() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            carBookingsService.searchCarBooking(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.carBookings = response.data;
                filterCarBookings(vm.searchText);
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

    CarBookingsController.$inject = injectParams;

    app.register.controller('CarBookingsController', CarBookingsController);

});
