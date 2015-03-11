'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'studentsService', 'modalService'];

    var StudentsController = function ($scope, $filter,authService, $location, $window,
        $timeout, studentsService, modalService) {

        var vm = this;
        vm.students = [];
        vm.filteredStudents = [];
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
            filterstudents(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getStudents();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getstudents();
        }


        function filterStudents(filterText) {
            
            vm.filteredStudents = $filter("studentFilter")(vm.students, filterText);
            vm.filteredCount = vm.filteredStudents.length;
        }

		vm.deleteStudent = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var student = getStudentById(id);
            var studentName = Student;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Student',
                headerText: 'Delete ' + studentName + '?',
                bodyText: 'Are you sure you want to delete this '+studentName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    studentsService.deleteStudent(id).then(function () {
                        for (var i = 0; i < vm.students.length; i++) {
                            if (vm.students[i].id === id) {
                                vm.students.splice(i, 1);
                                break;
                            }
							if (vm.filteredStudents[i].id === id) {
                                vm.filteredStudents.splice(i, 1);
                                break;
                            }
                        }
                        filterStudents(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting student: ' + error.message);
                    });
                }
            });
        };

		function getStudentsById(id) {
            for (var i = 0; i < vm.students.length; i++) {
                var student = vm.students[i];
                if (student.id === id) {
                    return student;
                }
            }
            return null;
        }

        function getStudents() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            studentsService.searchStudent(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.students = response.data;
                filterStudents(vm.searchText);
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

    StudentsController.$inject = injectParams;

    app.register.controller('StudentsController', StudentsController);

});
