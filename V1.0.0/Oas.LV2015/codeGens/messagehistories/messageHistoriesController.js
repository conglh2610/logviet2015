'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'messageHistoriesService', 'modalService'];

    var MessageHistoriesController = function ($scope, $filter,authService, $location, $window,
        $timeout, messageHistoriesService, modalService) {

        var vm = this;
        vm.messageHistories = [];
        vm.filteredMessageHistories = [];
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
            filtermessageHistories(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getMessageHistories();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getmessageHistories();
        }


        function filterMessageHistories(filterText) {
            
            vm.filteredMessageHistories = $filter("messageHistoryFilter")(vm.messageHistories, filterText);
            vm.filteredCount = vm.filteredMessageHistories.length;
        }

		vm.deleteMessageHistory = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var messageHistory = getMessageHistoryById(id);
            var messageHistoryName = MessageHistory;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete MessageHistory',
                headerText: 'Delete ' + messageHistoryName + '?',
                bodyText: 'Are you sure you want to delete this '+messageHistoryName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    messageHistoriesService.deleteMessageHistory(id).then(function () {
                        for (var i = 0; i < vm.messageHistories.length; i++) {
                            if (vm.messageHistories[i].id === id) {
                                vm.messageHistories.splice(i, 1);
                                break;
                            }
							if (vm.filteredMessageHistories[i].id === id) {
                                vm.filteredMessageHistories.splice(i, 1);
                                break;
                            }
                        }
                        filterMessageHistories(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting messageHistory: ' + error.message);
                    });
                }
            });
        };

		function getMessageHistoriesById(id) {
            for (var i = 0; i < vm.messageHistories.length; i++) {
                var messageHistory = vm.messageHistories[i];
                if (messageHistory.id === id) {
                    return messageHistory;
                }
            }
            return null;
        }

        function getMessageHistories() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            messageHistoriesService.searchMessageHistory(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.messageHistories = response.data;
                filterMessageHistories(vm.searchText);
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

    MessageHistoriesController.$inject = injectParams;

    app.register.controller('MessageHistoriesController', MessageHistoriesController);

});
