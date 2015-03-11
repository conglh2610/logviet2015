'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'parentsService', 'modalService'];

    var ParentsController = function ($scope, $filter,authService, $location, $window,
        $timeout, parentsService, modalService) {

        var vm = this;
        vm.parents = [];
        vm.filteredParents = [];
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
            filterparents(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getParents();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getparents();
        }


        function filterParents(filterText) {
            
            vm.filteredParents = $filter("parentFilter")(vm.parents, filterText);
            vm.filteredCount = vm.filteredParents.length;
        }

		vm.deleteParent = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var parent = getParentById(id);
            var parentName = Parent;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Parent',
                headerText: 'Delete ' + parentName + '?',
                bodyText: 'Are you sure you want to delete this '+parentName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    parentsService.deleteParent(id).then(function () {
                        for (var i = 0; i < vm.parents.length; i++) {
                            if (vm.parents[i].id === id) {
                                vm.parents.splice(i, 1);
                                break;
                            }
							if (vm.filteredParents[i].id === id) {
                                vm.filteredParents.splice(i, 1);
                                break;
                            }
                        }
                        filterParents(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting parent: ' + error.message);
                    });
                }
            });
        };

		function getParentsById(id) {
            for (var i = 0; i < vm.parents.length; i++) {
                var parent = vm.parents[i];
                if (parent.id === id) {
                    return parent;
                }
            }
            return null;
        }

        function getParents() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            parentsService.searchParent(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.parents = response.data;
                filterParents(vm.searchText);
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

    ParentsController.$inject = injectParams;

    app.register.controller('ParentsController', ParentsController);

});
