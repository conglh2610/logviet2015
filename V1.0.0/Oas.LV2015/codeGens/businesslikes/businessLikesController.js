'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'businessLikesService', 'modalService'];

    var BusinessLikesController = function ($scope, $filter,authService, $location, $window,
        $timeout, businessLikesService, modalService) {

        var vm = this;
        vm.businessLikes = [];
        vm.filteredBusinessLikes = [];
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
            filterbusinessLikes(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getBusinessLikes();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getbusinessLikes();
        }


        function filterBusinessLikes(filterText) {
            
            vm.filteredBusinessLikes = $filter("businessLikeFilter")(vm.businessLikes, filterText);
            vm.filteredCount = vm.filteredBusinessLikes.length;
        }

		vm.deleteBusinessLike = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var businessLike = getBusinessLikeById(id);
            var businessLikeName = BusinessLike;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete BusinessLike',
                headerText: 'Delete ' + businessLikeName + '?',
                bodyText: 'Are you sure you want to delete this '+businessLikeName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    businessLikesService.deleteBusinessLike(id).then(function () {
                        for (var i = 0; i < vm.businessLikes.length; i++) {
                            if (vm.businessLikes[i].id === id) {
                                vm.businessLikes.splice(i, 1);
                                break;
                            }
							if (vm.filteredBusinessLikes[i].id === id) {
                                vm.filteredBusinessLikes.splice(i, 1);
                                break;
                            }
                        }
                        filterBusinessLikes(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting businessLike: ' + error.message);
                    });
                }
            });
        };

		function getBusinessLikesById(id) {
            for (var i = 0; i < vm.businessLikes.length; i++) {
                var businessLike = vm.businessLikes[i];
                if (businessLike.id === id) {
                    return businessLike;
                }
            }
            return null;
        }

        function getBusinessLikes() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            businessLikesService.searchBusinessLike(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.businessLikes = response.data;
                filterBusinessLikes(vm.searchText);
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

    BusinessLikesController.$inject = injectParams;

    app.register.controller('BusinessLikesController', BusinessLikesController);

});
