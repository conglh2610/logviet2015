'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window', '$routeParams',
                        '$timeout', 'studentsService', 'modalService'];

    var StudentItemController = function ($scope, $location, $window, $routeParams,
        $timeout, studentsService, modalService) {

        var vm = this,
                    studentId = $routeParams.studentId,
                    timer,
                    onRouteChangeOff;
        vm.student = {};
        vm.states = [];
        vm.title = (studentId == '0') ? 'Add' : 'Edit';
        vm.buttonText = (studentId == '0') ? 'Add' : 'Update';
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.Save = function () {
            if (studentId != '0') {
                studentsService.updateStudent(vm.student).then(function () {
                    alert('Cập nhập thành công');

                    $location.path('/students');

                }, processError);
            }
            else {
                studentsService.insertStudent(vm.student).then(function () {
                    alert('Thêm mới thành công');

                    $location.path('/students');

                }, processError);
            }
        }

        function init() {
            if (studentId != '0') {
                studentsService.getStudent(studentId).then(function (student) {
                    vm.student = student;
                    vm.student.DateOfBirth = new Date(vm.student.DateOfBirth);
                }, processError);
            }
        }

        function processError(error) {
            vm.errorMessage = error.message;
            startTimer();
        }

        function startTimer() {
            timer = $timeout(function () {
                $timeout.cancel(timer);
                vm.errorMessage = '';
                vm.updateStatus = false;
            }, 3000);
        }

        init();

    };



    StudentItemController.$inject = injectParams;

    app.register.controller('StudentItemController', StudentItemController);
});