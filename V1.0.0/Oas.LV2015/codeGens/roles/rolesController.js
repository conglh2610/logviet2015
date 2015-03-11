'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'rolesService', 'modalService'];

    var RolesController = function ($scope, $filter,authService, $location, $window,
        $timeout, rolesService, modalService) {

        var vm = this;
        vm.roles = [];
        vm.filteredRoles = [];
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
            filterroles(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getRoles();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getroles();
        }


        function filterRoles(filterText) {
            
            vm.filteredRoles = $filter("roleFilter")(vm.roles, filterText);
            vm.filteredCount = vm.filteredRoles.length;
        }

		vm.deleteRole = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var role = getRoleById(id);
            var roleName = Role;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Role',
                headerText: 'Delete ' + roleName + '?',
                bodyText: 'Are you sure you want to delete this '+roleName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    rolesService.deleteRole(id).then(function () {
                        for (var i = 0; i < vm.roles.length; i++) {
                            if (vm.roles[i].id === id) {
                                vm.roles.splice(i, 1);
                                break;
                            }
							if (vm.filteredRoles[i].id === id) {
                                vm.filteredRoles.splice(i, 1);
                                break;
                            }
                        }
                        filterRoles(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting role: ' + error.message);
                    });
                }
            });
        };

		function getRolesById(id) {
            for (var i = 0; i < vm.roles.length; i++) {
                var role = vm.roles[i];
                if (role.id === id) {
                    return role;
                }
            }
            return null;
        }

        function getRoles() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            rolesService.searchRole(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.roles = response.data;
                filterRoles(vm.searchText);
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

    RolesController.$inject = injectParams;

    app.register.controller('RolesController', RolesController);

});
