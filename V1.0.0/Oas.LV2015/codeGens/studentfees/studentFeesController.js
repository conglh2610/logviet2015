'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'studentFeesService', 'modalService'];

    var StudentFeesController = function ($scope, $filter,authService, $location, $window,
        $timeout, studentFeesService, modalService) {

        var vm = this;
        vm.studentFees = [];
        vm.filteredStudentFees = [];
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
            filterstudentFees(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getStudentFees();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getstudentFees();
        }


        function filterStudentFees(filterText) {
            
            vm.filteredStudentFees = $filter("studentFeeFilter")(vm.studentFees, filterText);
            vm.filteredCount = vm.filteredStudentFees.length;
        }

		vm.deleteStudentFee = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var studentFee = getStudentFeeById(id);
            var studentFeeName = StudentFee;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete StudentFee',
                headerText: 'Delete ' + studentFeeName + '?',
                bodyText: 'Are you sure you want to delete this '+studentFeeName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    studentFeesService.deleteStudentFee(id).then(function () {
                        for (var i = 0; i < vm.studentFees.length; i++) {
                            if (vm.studentFees[i].id === id) {
                                vm.studentFees.splice(i, 1);
                                break;
                            }
							if (vm.filteredStudentFees[i].id === id) {
                                vm.filteredStudentFees.splice(i, 1);
                                break;
                            }
                        }
                        filterStudentFees(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting studentFee: ' + error.message);
                    });
                }
            });
        };

		function getStudentFeesById(id) {
            for (var i = 0; i < vm.studentFees.length; i++) {
                var studentFee = vm.studentFees[i];
                if (studentFee.id === id) {
                    return studentFee;
                }
            }
            return null;
        }

        function getStudentFees() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            studentFeesService.searchStudentFee(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.studentFees = response.data;
                filterStudentFees(vm.searchText);
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

    StudentFeesController.$inject = injectParams;

    app.register.controller('StudentFeesController', StudentFeesController);

});
