'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window',
                        '$timeout', 'studentsService', 'modalService'];

    var StudentsController = function ($scope, $location, $window,
        $timeout, studentsService, modalService) {

        var vm = this;
        vm.students = [];
        vm.errorMessage = "";

        //Paging
        vm.currentPage = 1;
        vm.itemPerPage = 10;
        vm.totalRecords = 0;
        vm.columnName = "name";
        vm.sortDirection = false;

        vm.searchText = "";
        vm.cardAnimationClass = '.card-animation';
        vm.DisplayModeEnum = {
            Card: 0,
            List: 1
        };

        vm.Save = function () {
            if ($scope.studentform.$valid) {
                studentsService.insertStudent(vm.student).then(processSuccess, processError);
            }
        };

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getStudents();
        }

        vm.searchTextChanged = function () {
            vm.currentPage = 1;
            getStudents();
        }

        vm.setOrder = function (columnName) {
            if (columnName === vm.columnName) {
                vm.sortDirection = !vm.sortDirection;
            }
            vm.columnName = columnName;

            vm.currentPage = 1;
            getStudents();
        }

        vm.deleteStudent = function (id) {

            var student = getStudentById(id);

            var studentName = student.FirstName + ' ' + student.LastName;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Student',
                headerText: 'Delete ' + studentName + '?',
                bodyText: 'Bạn có muốn xóa học viên này không?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    studentsService.deleteStudent(id).then(function () {
                        initalData();
                    }, function (error) {
                        $window.alert('Error deleting customer: ' + error.message);
                    });
                }
            });
        }

        vm.navigate = function (url) {
            $location.path(url);
        };

        vm.viewStudentClassHistory = function (id) {
            var url = "/studentClassHistory/" + id;
            $location.path(url);
        }

        function initalData() {
            vm.currentPage = 1;
            getStudents();
        }



        function processSuccess() {
            $scope.studentform.$dirty = false;

            alert('Save success');
        };

        function processError(error) {
            vm.errorMessage = error.message;

            alert(error);
        };

        /*khoa added*/
        function getStudents() {

            var criteria = {
                CurrentPage: vm.currentPage - 1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection,
                Name: vm.searchText,
            };


            studentsService.search(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.students = response.data;
            });

        }

        function getStudentById(id) {

            for (var i = 0; i < vm.students.length; i++) {
                var student = vm.students[i];
                if (student.Id === id) {
                    return student;
                }
            }
            return null;
        }

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