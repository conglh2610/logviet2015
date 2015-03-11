'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'programsService', 'modalService'];

    var ProgramsController = function ($scope, $filter,authService, $location, $window,
        $timeout, programsService, modalService) {

        var vm = this;
        vm.programs = [];
        vm.filteredPrograms = [];
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
            filterprograms(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getPrograms();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getprograms();
        }


        function filterPrograms(filterText) {
            
            vm.filteredPrograms = $filter("programFilter")(vm.programs, filterText);
            vm.filteredCount = vm.filteredPrograms.length;
        }

		vm.deleteProgram = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var program = getProgramById(id);
            var programName = Program;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Program',
                headerText: 'Delete ' + programName + '?',
                bodyText: 'Are you sure you want to delete this '+programName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    programsService.deleteProgram(id).then(function () {
                        for (var i = 0; i < vm.programs.length; i++) {
                            if (vm.programs[i].id === id) {
                                vm.programs.splice(i, 1);
                                break;
                            }
							if (vm.filteredPrograms[i].id === id) {
                                vm.filteredPrograms.splice(i, 1);
                                break;
                            }
                        }
                        filterPrograms(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting program: ' + error.message);
                    });
                }
            });
        };

		function getProgramsById(id) {
            for (var i = 0; i < vm.programs.length; i++) {
                var program = vm.programs[i];
                if (program.id === id) {
                    return program;
                }
            }
            return null;
        }

        function getPrograms() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            programsService.searchProgram(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.programs = response.data;
                filterPrograms(vm.searchText);
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

    ProgramsController.$inject = injectParams;

    app.register.controller('ProgramsController', ProgramsController);

});
