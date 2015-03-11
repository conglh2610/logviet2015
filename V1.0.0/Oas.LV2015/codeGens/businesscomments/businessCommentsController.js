'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'businessCommentsService', 'modalService'];

    var BusinessCommentsController = function ($scope, $filter,authService, $location, $window,
        $timeout, businessCommentsService, modalService) {

        var vm = this;
        vm.businessComments = [];
        vm.filteredBusinessComments = [];
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
            filterbusinessComments(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getBusinessComments();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getbusinessComments();
        }


        function filterBusinessComments(filterText) {
            
            vm.filteredBusinessComments = $filter("businessCommentFilter")(vm.businessComments, filterText);
            vm.filteredCount = vm.filteredBusinessComments.length;
        }

		vm.deleteBusinessComment = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var businessComment = getBusinessCommentById(id);
            var businessCommentName = BusinessComment;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete BusinessComment',
                headerText: 'Delete ' + businessCommentName + '?',
                bodyText: 'Are you sure you want to delete this '+businessCommentName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    businessCommentsService.deleteBusinessComment(id).then(function () {
                        for (var i = 0; i < vm.businessComments.length; i++) {
                            if (vm.businessComments[i].id === id) {
                                vm.businessComments.splice(i, 1);
                                break;
                            }
							if (vm.filteredBusinessComments[i].id === id) {
                                vm.filteredBusinessComments.splice(i, 1);
                                break;
                            }
                        }
                        filterBusinessComments(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting businessComment: ' + error.message);
                    });
                }
            });
        };

		function getBusinessCommentsById(id) {
            for (var i = 0; i < vm.businessComments.length; i++) {
                var businessComment = vm.businessComments[i];
                if (businessComment.id === id) {
                    return businessComment;
                }
            }
            return null;
        }

        function getBusinessComments() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            businessCommentsService.searchBusinessComment(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.businessComments = response.data;
                filterBusinessComments(vm.searchText);
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

    BusinessCommentsController.$inject = injectParams;

    app.register.controller('BusinessCommentsController', BusinessCommentsController);

});
