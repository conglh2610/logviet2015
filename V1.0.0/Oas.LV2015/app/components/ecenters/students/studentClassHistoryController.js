'use strict'

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window', '$routeParams',
                        '$timeout', 'studentsService', 'modalService'];

    var StudentClassHistoryController = function ($scope, $location, $window, $routeParams,
        $timeout, studentsService, modalService) {

        var vm = this;
        vm.studentId = $routeParams.studentId;

        //Start Data
        vm.studentClassHistories = {};
        //End Data

        //Start Paging
        vm.currentPage = 1;
        vm.itemPerPage = 10;
        vm.sortDirection = false;
        vm.columnName = "";
        vm.totalRecords = 0;
        //End Pagin

        function initalData() {
            vm.currentPage = 1;
            getStudentClassHistories();
        }

        function getStudentClassHistories() {

            var criteria = {
                CurrentPage: vm.currentPage - 1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection,
                Name: vm.searchText,
            };

            studentsService.getStudentClassHistories(vm.studentId, criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.studentClassHistories = response.data;
            });
        }

        initalData();
    }

    StudentClassHistoryController.$inject = injectParams;

    app.register.controller('StudentClassHistoryController', StudentClassHistoryController);
});