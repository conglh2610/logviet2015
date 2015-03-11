'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'bookingNotesService', 'modalService'];

    var BookingNotesController = function ($scope, $filter,authService, $location, $window,
        $timeout, bookingNotesService, modalService) {

        var vm = this;
        vm.bookingNotes = [];
        vm.filteredBookingNotes = [];
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
            filterbookingNotes(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getBookingNotes();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getbookingNotes();
        }


        function filterBookingNotes(filterText) {
            
            vm.filteredBookingNotes = $filter("bookingNoteFilter")(vm.bookingNotes, filterText);
            vm.filteredCount = vm.filteredBookingNotes.length;
        }

		vm.deleteBookingNote = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var bookingNote = getBookingNoteById(id);
            var bookingNoteName = BookingNote;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete BookingNote',
                headerText: 'Delete ' + bookingNoteName + '?',
                bodyText: 'Are you sure you want to delete this '+bookingNoteName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    bookingNotesService.deleteBookingNote(id).then(function () {
                        for (var i = 0; i < vm.bookingNotes.length; i++) {
                            if (vm.bookingNotes[i].id === id) {
                                vm.bookingNotes.splice(i, 1);
                                break;
                            }
							if (vm.filteredBookingNotes[i].id === id) {
                                vm.filteredBookingNotes.splice(i, 1);
                                break;
                            }
                        }
                        filterBookingNotes(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting bookingNote: ' + error.message);
                    });
                }
            });
        };

		function getBookingNotesById(id) {
            for (var i = 0; i < vm.bookingNotes.length; i++) {
                var bookingNote = vm.bookingNotes[i];
                if (bookingNote.id === id) {
                    return bookingNote;
                }
            }
            return null;
        }

        function getBookingNotes() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            bookingNotesService.searchBookingNote(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.bookingNotes = response.data;
                filterBookingNotes(vm.searchText);
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

    BookingNotesController.$inject = injectParams;

    app.register.controller('BookingNotesController', BookingNotesController);

});
