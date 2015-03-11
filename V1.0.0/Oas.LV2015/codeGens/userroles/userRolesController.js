'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'userRolesService', 'modalService'];

    var UserRolesController = function ($scope, $filter,authService, $location, $window,
        $timeout, userRolesService, modalService) {

        var vm = this;
        vm.userRoles = [];
        vm.filteredUserRoles = [];
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
            filteruserRoles(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getUserRoles();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getuserRoles();
        }


        function filterUserRoles(filterText) {
            
            vm.filteredUserRoles = $filter("userRoleFilter")(vm.userRoles, filterText);
            vm.filteredCount = vm.filteredUserRoles.length;
        }

		vm.deleteUserRole = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var userRole = getUserRoleById(id);
            var userRoleName = UserRole;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete UserRole',
                headerText: 'Delete ' + userRoleName + '?',
                bodyText: 'Are you sure you want to delete this '+userRoleName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    userRolesService.deleteUserRole(id).then(function () {
                        for (var i = 0; i < vm.userRoles.length; i++) {
                            if (vm.userRoles[i].id === id) {
                                vm.userRoles.splice(i, 1);
                                break;
                            }
							if (vm.filteredUserRoles[i].id === id) {
                                vm.filteredUserRoles.splice(i, 1);
                                break;
                            }
                        }
                        filterUserRoles(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting userRole: ' + error.message);
                    });
                }
            });
        };

		function getUserRolesById(id) {
            for (var i = 0; i < vm.userRoles.length; i++) {
                var userRole = vm.userRoles[i];
                if (userRole.id === id) {
                    return userRole;
                }
            }
            return null;
        }

        function getUserRoles() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            userRolesService.searchUserRole(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.userRoles = response.data;
                filterUserRoles(vm.searchText);
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

    UserRolesController.$inject = injectParams;

    app.register.controller('UserRolesController', UserRolesController);

});
