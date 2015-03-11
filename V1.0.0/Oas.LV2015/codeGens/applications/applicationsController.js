'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'applicationsService', 'modalService'];

    var ApplicationsController = function ($scope, $filter,authService, $location, $window,
        $timeout, applicationsService, modalService) {

        var vm = this;
        vm.applications = [];
        vm.filteredApplications = [];
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
            filterapplications(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getApplications();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getapplications();
        }


        function filterApplications(filterText) {
            
            vm.filteredApplications = $filter("applicationFilter")(vm.applications, filterText);
            vm.filteredCount = vm.filteredApplications.length;
        }

		vm.deleteApplication = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var application = getApplicationById(id);
            var applicationName = Application;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Application',
                headerText: 'Delete ' + applicationName + '?',
                bodyText: 'Are you sure you want to delete this '+applicationName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    applicationsService.deleteApplication(id).then(function () {
                        for (var i = 0; i < vm.applications.length; i++) {
                            if (vm.applications[i].id === id) {
                                vm.applications.splice(i, 1);
                                break;
                            }
							if (vm.filteredApplications[i].id === id) {
                                vm.filteredApplications.splice(i, 1);
                                break;
                            }
                        }
                        filterApplications(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting application: ' + error.message);
                    });
                }
            });
        };

		function getApplicationsById(id) {
            for (var i = 0; i < vm.applications.length; i++) {
                var application = vm.applications[i];
                if (application.id === id) {
                    return application;
                }
            }
            return null;
        }

        function getApplications() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            applicationsService.searchApplication(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.applications = response.data;
                filterApplications(vm.searchText);
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

    ApplicationsController.$inject = injectParams;

    app.register.controller('ApplicationsController', ApplicationsController);

});
