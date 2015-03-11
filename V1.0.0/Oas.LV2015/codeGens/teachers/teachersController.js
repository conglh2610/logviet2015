'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'teachersService', 'modalService'];

    var TeachersController = function ($scope, $filter,authService, $location, $window,
        $timeout, teachersService, modalService) {

        var vm = this;
        vm.teachers = [];
        vm.filteredTeachers = [];
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
            filterteachers(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getTeachers();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getteachers();
        }


        function filterTeachers(filterText) {
            
            vm.filteredTeachers = $filter("teacherFilter")(vm.teachers, filterText);
            vm.filteredCount = vm.filteredTeachers.length;
        }

		vm.deleteTeacher = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var teacher = getTeacherById(id);
            var teacherName = Teacher;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Teacher',
                headerText: 'Delete ' + teacherName + '?',
                bodyText: 'Are you sure you want to delete this '+teacherName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    teachersService.deleteTeacher(id).then(function () {
                        for (var i = 0; i < vm.teachers.length; i++) {
                            if (vm.teachers[i].id === id) {
                                vm.teachers.splice(i, 1);
                                break;
                            }
							if (vm.filteredTeachers[i].id === id) {
                                vm.filteredTeachers.splice(i, 1);
                                break;
                            }
                        }
                        filterTeachers(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting teacher: ' + error.message);
                    });
                }
            });
        };

		function getTeachersById(id) {
            for (var i = 0; i < vm.teachers.length; i++) {
                var teacher = vm.teachers[i];
                if (teacher.id === id) {
                    return teacher;
                }
            }
            return null;
        }

        function getTeachers() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            teachersService.searchTeacher(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.teachers = response.data;
                filterTeachers(vm.searchText);
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

    TeachersController.$inject = injectParams;

    app.register.controller('TeachersController', TeachersController);

});
