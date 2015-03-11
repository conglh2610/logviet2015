'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'classesService', 'modalService'];

    var ClassesController = function ($scope, $filter,authService, $location, $window,
        $timeout, classesService, modalService) {

        var vm = this;
        vm.classes = [];
        vm.filteredClasses = [];
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
            filterclasses(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getClasses();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getclasses();
        }


        function filterClasses(filterText) {
            
            vm.filteredClasses = $filter("classFilter")(vm.classes, filterText);
            vm.filteredCount = vm.filteredClasses.length;
        }

		vm.deleteClass = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var class = getClassById(id);
            var className = Class;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Class',
                headerText: 'Delete ' + className + '?',
                bodyText: 'Are you sure you want to delete this '+className+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    classesService.deleteClass(id).then(function () {
                        for (var i = 0; i < vm.classes.length; i++) {
                            if (vm.classes[i].id === id) {
                                vm.classes.splice(i, 1);
                                break;
                            }
							if (vm.filteredClasses[i].id === id) {
                                vm.filteredClasses.splice(i, 1);
                                break;
                            }
                        }
                        filterClasses(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting class: ' + error.message);
                    });
                }
            });
        };

		function getClassesById(id) {
            for (var i = 0; i < vm.classes.length; i++) {
                var class = vm.classes[i];
                if (class.id === id) {
                    return class;
                }
            }
            return null;
        }

        function getClasses() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            classesService.searchClass(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.classes = response.data;
                filterClasses(vm.searchText);
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

    ClassesController.$inject = injectParams;

    app.register.controller('ClassesController', ClassesController);

});
