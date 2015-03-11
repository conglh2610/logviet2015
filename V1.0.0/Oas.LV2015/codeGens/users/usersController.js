'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'usersService', 'modalService'];

    var UsersController = function ($scope, $filter,authService, $location, $window,
        $timeout, usersService, modalService) {

        var vm = this;
        vm.users = [];
        vm.filteredUsers = [];
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
            filterusers(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getUsers();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getusers();
        }


        function filterUsers(filterText) {
            
            vm.filteredUsers = $filter("userFilter")(vm.users, filterText);
            vm.filteredCount = vm.filteredUsers.length;
        }

		vm.deleteUser = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var user = getUserById(id);
            var userName = User;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete User',
                headerText: 'Delete ' + userName + '?',
                bodyText: 'Are you sure you want to delete this '+userName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    usersService.deleteUser(id).then(function () {
                        for (var i = 0; i < vm.users.length; i++) {
                            if (vm.users[i].id === id) {
                                vm.users.splice(i, 1);
                                break;
                            }
							if (vm.filteredUsers[i].id === id) {
                                vm.filteredUsers.splice(i, 1);
                                break;
                            }
                        }
                        filterUsers(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting user: ' + error.message);
                    });
                }
            });
        };

		function getUsersById(id) {
            for (var i = 0; i < vm.users.length; i++) {
                var user = vm.users[i];
                if (user.id === id) {
                    return user;
                }
            }
            return null;
        }

        function getUsers() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            usersService.searchUser(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.users = response.data;
                filterUsers(vm.searchText);
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

    UsersController.$inject = injectParams;

    app.register.controller('UsersController', UsersController);

});
