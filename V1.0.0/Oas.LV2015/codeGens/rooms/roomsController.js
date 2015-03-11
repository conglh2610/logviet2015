'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'roomsService', 'modalService'];

    var RoomsController = function ($scope, $filter,authService, $location, $window,
        $timeout, roomsService, modalService) {

        var vm = this;
        vm.rooms = [];
        vm.filteredRooms = [];
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
            filterrooms(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getRooms();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getrooms();
        }


        function filterRooms(filterText) {
            
            vm.filteredRooms = $filter("roomFilter")(vm.rooms, filterText);
            vm.filteredCount = vm.filteredRooms.length;
        }

		vm.deleteRoom = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var room = getRoomById(id);
            var roomName = Room;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Room',
                headerText: 'Delete ' + roomName + '?',
                bodyText: 'Are you sure you want to delete this '+roomName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    roomsService.deleteRoom(id).then(function () {
                        for (var i = 0; i < vm.rooms.length; i++) {
                            if (vm.rooms[i].id === id) {
                                vm.rooms.splice(i, 1);
                                break;
                            }
							if (vm.filteredRooms[i].id === id) {
                                vm.filteredRooms.splice(i, 1);
                                break;
                            }
                        }
                        filterRooms(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting room: ' + error.message);
                    });
                }
            });
        };

		function getRoomsById(id) {
            for (var i = 0; i < vm.rooms.length; i++) {
                var room = vm.rooms[i];
                if (room.id === id) {
                    return room;
                }
            }
            return null;
        }

        function getRooms() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            roomsService.searchRoom(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.rooms = response.data;
                filterRooms(vm.searchText);
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

    RoomsController.$inject = injectParams;

    app.register.controller('RoomsController', RoomsController);

});
