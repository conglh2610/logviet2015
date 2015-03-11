'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'areasService', 'modalService'];

    var AreasController = function ($scope, $filter,authService, $location, $window,
        $timeout, areasService, modalService) {

        var vm = this;
        vm.areas = [];
        vm.filteredAreas = [];
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
            filterareas(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getAreas();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getareas();
        }


        function filterAreas(filterText) {
            
            vm.filteredAreas = $filter("areaFilter")(vm.areas, filterText);
            vm.filteredCount = vm.filteredAreas.length;
        }

		vm.deleteArea = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var area = getAreaById(id);
            var areaName = Area;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Area',
                headerText: 'Delete ' + areaName + '?',
                bodyText: 'Are you sure you want to delete this '+areaName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    areasService.deleteArea(id).then(function () {
                        for (var i = 0; i < vm.areas.length; i++) {
                            if (vm.areas[i].id === id) {
                                vm.areas.splice(i, 1);
                                break;
                            }
							if (vm.filteredAreas[i].id === id) {
                                vm.filteredAreas.splice(i, 1);
                                break;
                            }
                        }
                        filterAreas(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting area: ' + error.message);
                    });
                }
            });
        };

		function getAreasById(id) {
            for (var i = 0; i < vm.areas.length; i++) {
                var area = vm.areas[i];
                if (area.id === id) {
                    return area;
                }
            }
            return null;
        }

        function getAreas() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            areasService.searchArea(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.areas = response.data;
                filterAreas(vm.searchText);
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

    AreasController.$inject = injectParams;

    app.register.controller('AreasController', AreasController);

});
