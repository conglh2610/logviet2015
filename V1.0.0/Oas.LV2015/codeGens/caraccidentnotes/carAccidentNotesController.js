'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'carAccidentNotesService', 'modalService'];

    var CarAccidentNotesController = function ($scope, $filter,authService, $location, $window,
        $timeout, carAccidentNotesService, modalService) {

        var vm = this;
        vm.carAccidentNotes = [];
        vm.filteredCarAccidentNotes = [];
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
            filtercarAccidentNotes(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCarAccidentNotes();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcarAccidentNotes();
        }


        function filterCarAccidentNotes(filterText) {
            
            vm.filteredCarAccidentNotes = $filter("carAccidentNoteFilter")(vm.carAccidentNotes, filterText);
            vm.filteredCount = vm.filteredCarAccidentNotes.length;
        }

		vm.deleteCarAccidentNote = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var carAccidentNote = getCarAccidentNoteById(id);
            var carAccidentNoteName = CarAccidentNote;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete CarAccidentNote',
                headerText: 'Delete ' + carAccidentNoteName + '?',
                bodyText: 'Are you sure you want to delete this '+carAccidentNoteName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    carAccidentNotesService.deleteCarAccidentNote(id).then(function () {
                        for (var i = 0; i < vm.carAccidentNotes.length; i++) {
                            if (vm.carAccidentNotes[i].id === id) {
                                vm.carAccidentNotes.splice(i, 1);
                                break;
                            }
							if (vm.filteredCarAccidentNotes[i].id === id) {
                                vm.filteredCarAccidentNotes.splice(i, 1);
                                break;
                            }
                        }
                        filterCarAccidentNotes(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting carAccidentNote: ' + error.message);
                    });
                }
            });
        };

		function getCarAccidentNotesById(id) {
            for (var i = 0; i < vm.carAccidentNotes.length; i++) {
                var carAccidentNote = vm.carAccidentNotes[i];
                if (carAccidentNote.id === id) {
                    return carAccidentNote;
                }
            }
            return null;
        }

        function getCarAccidentNotes() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            carAccidentNotesService.searchCarAccidentNote(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.carAccidentNotes = response.data;
                filterCarAccidentNotes(vm.searchText);
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

    CarAccidentNotesController.$inject = injectParams;

    app.register.controller('CarAccidentNotesController', CarAccidentNotesController);

});
