'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'schedulersService', 'modalService'];

    var SchedulersController = function ($scope, $filter,authService, $location, $window,
        $timeout, schedulersService, modalService) {

        var vm = this;
        vm.schedulers = [];
        vm.filteredSchedulers = [];
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
            filterschedulers(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getSchedulers();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getschedulers();
        }


        function filterSchedulers(filterText) {
            
            vm.filteredSchedulers = $filter("schedulerFilter")(vm.schedulers, filterText);
            vm.filteredCount = vm.filteredSchedulers.length;
        }

		vm.deleteScheduler = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var scheduler = getSchedulerById(id);
            var schedulerName = Scheduler;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Scheduler',
                headerText: 'Delete ' + schedulerName + '?',
                bodyText: 'Are you sure you want to delete this '+schedulerName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    schedulersService.deleteScheduler(id).then(function () {
                        for (var i = 0; i < vm.schedulers.length; i++) {
                            if (vm.schedulers[i].id === id) {
                                vm.schedulers.splice(i, 1);
                                break;
                            }
							if (vm.filteredSchedulers[i].id === id) {
                                vm.filteredSchedulers.splice(i, 1);
                                break;
                            }
                        }
                        filterSchedulers(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting scheduler: ' + error.message);
                    });
                }
            });
        };

		function getSchedulersById(id) {
            for (var i = 0; i < vm.schedulers.length; i++) {
                var scheduler = vm.schedulers[i];
                if (scheduler.id === id) {
                    return scheduler;
                }
            }
            return null;
        }

        function getSchedulers() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            schedulersService.searchScheduler(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.schedulers = response.data;
                filterSchedulers(vm.searchText);
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

    SchedulersController.$inject = injectParams;

    app.register.controller('SchedulersController', SchedulersController);

});
