'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'userApplicationsService', 'modalService'];

    var UserApplicationsController = function ($scope, $filter,authService, $location, $window,
        $timeout, userApplicationsService, modalService) {

        var vm = this;
        vm.userApplications = [];
        vm.filteredUserApplications = [];
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
            filteruserApplications(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getUserApplications();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getuserApplications();
        }


        function filterUserApplications(filterText) {
            
            vm.filteredUserApplications = $filter("userApplicationFilter")(vm.userApplications, filterText);
            vm.filteredCount = vm.filteredUserApplications.length;
        }

		vm.deleteUserApplication = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var userApplication = getUserApplicationById(id);
            var userApplicationName = UserApplication;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete UserApplication',
                headerText: 'Delete ' + userApplicationName + '?',
                bodyText: 'Are you sure you want to delete this '+userApplicationName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    userApplicationsService.deleteUserApplication(id).then(function () {
                        for (var i = 0; i < vm.userApplications.length; i++) {
                            if (vm.userApplications[i].id === id) {
                                vm.userApplications.splice(i, 1);
                                break;
                            }
							if (vm.filteredUserApplications[i].id === id) {
                                vm.filteredUserApplications.splice(i, 1);
                                break;
                            }
                        }
                        filterUserApplications(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting userApplication: ' + error.message);
                    });
                }
            });
        };

		function getUserApplicationsById(id) {
            for (var i = 0; i < vm.userApplications.length; i++) {
                var userApplication = vm.userApplications[i];
                if (userApplication.id === id) {
                    return userApplication;
                }
            }
            return null;
        }

        function getUserApplications() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            userApplicationsService.searchUserApplication(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.userApplications = response.data;
                filterUserApplications(vm.searchText);
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

    UserApplicationsController.$inject = injectParams;

    app.register.controller('UserApplicationsController', UserApplicationsController);

});
