'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'classTeachersService', 'modalService'];

    var ClassTeachersController = function ($scope, $filter,authService, $location, $window,
        $timeout, classTeachersService, modalService) {

        var vm = this;
        vm.classTeachers = [];
        vm.filteredClassTeachers = [];
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
            filterclassTeachers(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getClassTeachers();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getclassTeachers();
        }


        function filterClassTeachers(filterText) {
            
            vm.filteredClassTeachers = $filter("classTeacherFilter")(vm.classTeachers, filterText);
            vm.filteredCount = vm.filteredClassTeachers.length;
        }

		vm.deleteClassTeacher = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var classTeacher = getClassTeacherById(id);
            var classTeacherName = ClassTeacher;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete ClassTeacher',
                headerText: 'Delete ' + classTeacherName + '?',
                bodyText: 'Are you sure you want to delete this '+classTeacherName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    classTeachersService.deleteClassTeacher(id).then(function () {
                        for (var i = 0; i < vm.classTeachers.length; i++) {
                            if (vm.classTeachers[i].id === id) {
                                vm.classTeachers.splice(i, 1);
                                break;
                            }
							if (vm.filteredClassTeachers[i].id === id) {
                                vm.filteredClassTeachers.splice(i, 1);
                                break;
                            }
                        }
                        filterClassTeachers(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting classTeacher: ' + error.message);
                    });
                }
            });
        };

		function getClassTeachersById(id) {
            for (var i = 0; i < vm.classTeachers.length; i++) {
                var classTeacher = vm.classTeachers[i];
                if (classTeacher.id === id) {
                    return classTeacher;
                }
            }
            return null;
        }

        function getClassTeachers() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            classTeachersService.searchClassTeacher(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.classTeachers = response.data;
                filterClassTeachers(vm.searchText);
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

    ClassTeachersController.$inject = injectParams;

    app.register.controller('ClassTeachersController', ClassTeachersController);

});
