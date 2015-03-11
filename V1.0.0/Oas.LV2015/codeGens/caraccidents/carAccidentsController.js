'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'carAccidentsService', 'modalService'];

    var CarAccidentsController = function ($scope, $filter,authService, $location, $window,
        $timeout, carAccidentsService, modalService) {

        var vm = this;
        vm.carAccidents = [];
        vm.filteredCarAccidents = [];
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
            filtercarAccidents(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCarAccidents();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcarAccidents();
        }


        function filterCarAccidents(filterText) {
            
            vm.filteredCarAccidents = $filter("carAccidentFilter")(vm.carAccidents, filterText);
            vm.filteredCount = vm.filteredCarAccidents.length;
        }

		vm.deleteCarAccident = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var carAccident = getCarAccidentById(id);
            var carAccidentName = CarAccident;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete CarAccident',
                headerText: 'Delete ' + carAccidentName + '?',
                bodyText: 'Are you sure you want to delete this '+carAccidentName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carAccidentsService.deleteCarAccident(id).then(function () {
                        for (var i = 0; i < vm.carAccidents.length; i++) {
                            if (vm.carAccidents[i].id === id) {
                                vm.carAccidents.splice(i, 1);
                                break;
                            }
							if (vm.filteredCarAccidents[i].id === id) {
                                vm.filteredCarAccidents.splice(i, 1);
                                break;
                            }
                        }
                        filterCarAccidents(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting carAccident: ' + error.message);
                    });
                }
            });
        };

		function getCarAccidentsById(id) {
            for (var i = 0; i < vm.carAccidents.length; i++) {
                var carAccident = vm.carAccidents[i];
                if (carAccident.id === id) {
                    return carAccident;
                }
            }
            return null;
        }

        function getCarAccidents() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            carAccidentsService.searchCarAccident(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.carAccidents = response.data;
                filterCarAccidents(vm.searchText);
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

    CarAccidentsController.$inject = injectParams;

    app.register.controller('CarAccidentsController', CarAccidentsController);

});
