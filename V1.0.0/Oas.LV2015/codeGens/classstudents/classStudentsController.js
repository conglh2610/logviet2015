'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'classStudentsService', 'modalService'];

    var ClassStudentsController = function ($scope, $filter,authService, $location, $window,
        $timeout, classStudentsService, modalService) {

        var vm = this;
        vm.classStudents = [];
        vm.filteredClassStudents = [];
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
            filterclassStudents(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getClassStudents();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getclassStudents();
        }


        function filterClassStudents(filterText) {
            
            vm.filteredClassStudents = $filter("classStudentFilter")(vm.classStudents, filterText);
            vm.filteredCount = vm.filteredClassStudents.length;
        }

		vm.deleteClassStudent = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var classStudent = getClassStudentById(id);
            var classStudentName = ClassStudent;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete ClassStudent',
                headerText: 'Delete ' + classStudentName + '?',
                bodyText: 'Are you sure you want to delete this '+classStudentName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    classStudentsService.deleteClassStudent(id).then(function () {
                        for (var i = 0; i < vm.classStudents.length; i++) {
                            if (vm.classStudents[i].id === id) {
                                vm.classStudents.splice(i, 1);
                                break;
                            }
							if (vm.filteredClassStudents[i].id === id) {
                                vm.filteredClassStudents.splice(i, 1);
                                break;
                            }
                        }
                        filterClassStudents(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting classStudent: ' + error.message);
                    });
                }
            });
        };

		function getClassStudentsById(id) {
            for (var i = 0; i < vm.classStudents.length; i++) {
                var classStudent = vm.classStudents[i];
                if (classStudent.id === id) {
                    return classStudent;
                }
            }
            return null;
        }

        function getClassStudents() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            classStudentsService.searchClassStudent(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.classStudents = response.data;
                filterClassStudents(vm.searchText);
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

    ClassStudentsController.$inject = injectParams;

    app.register.controller('ClassStudentsController', ClassStudentsController);

});
