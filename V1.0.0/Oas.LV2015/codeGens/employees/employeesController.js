'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'employeesService', 'modalService'];

    var EmployeesController = function ($scope, $filter,authService, $location, $window,
        $timeout, employeesService, modalService) {

        var vm = this;
        vm.employees = [];
        vm.filteredEmployees = [];
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
            filteremployees(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getEmployees();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getemployees();
        }


        function filterEmployees(filterText) {
            
            vm.filteredEmployees = $filter("employeeFilter")(vm.employees, filterText);
            vm.filteredCount = vm.filteredEmployees.length;
        }

		vm.deleteEmployee = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var employee = getEmployeeById(id);
            var employeeName = Employee;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Employee',
                headerText: 'Delete ' + employeeName + '?',
                bodyText: 'Are you sure you want to delete this '+employeeName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    employeesService.deleteEmployee(id).then(function () {
                        for (var i = 0; i < vm.employees.length; i++) {
                            if (vm.employees[i].id === id) {
                                vm.employees.splice(i, 1);
                                break;
                            }
							if (vm.filteredEmployees[i].id === id) {
                                vm.filteredEmployees.splice(i, 1);
                                break;
                            }
                        }
                        filterEmployees(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting employee: ' + error.message);
                    });
                }
            });
        };

		function getEmployeesById(id) {
            for (var i = 0; i < vm.employees.length; i++) {
                var employee = vm.employees[i];
                if (employee.id === id) {
                    return employee;
                }
            }
            return null;
        }

        function getEmployees() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            employeesService.searchEmployee(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.employees = response.data;
                filterEmployees(vm.searchText);
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

    EmployeesController.$inject = injectParams;

    app.register.controller('EmployeesController', EmployeesController);

});
