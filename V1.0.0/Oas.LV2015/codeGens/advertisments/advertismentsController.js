'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'advertismentsService', 'modalService'];

    var AdvertismentsController = function ($scope, $filter,authService, $location, $window,
        $timeout, advertismentsService, modalService) {

        var vm = this;
        vm.advertisments = [];
        vm.filteredAdvertisments = [];
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
            filteradvertisments(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getAdvertisments();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getadvertisments();
        }


        function filterAdvertisments(filterText) {
            
            vm.filteredAdvertisments = $filter("advertismentFilter")(vm.advertisments, filterText);
            vm.filteredCount = vm.filteredAdvertisments.length;
        }

		vm.deleteAdvertisment = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var advertisment = getAdvertismentById(id);
            var advertismentName = Advertisment;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Advertisment',
                headerText: 'Delete ' + advertismentName + '?',
                bodyText: 'Are you sure you want to delete this '+advertismentName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    advertismentsService.deleteAdvertisment(id).then(function () {
                        for (var i = 0; i < vm.advertisments.length; i++) {
                            if (vm.advertisments[i].id === id) {
                                vm.advertisments.splice(i, 1);
                                break;
                            }
							if (vm.filteredAdvertisments[i].id === id) {
                                vm.filteredAdvertisments.splice(i, 1);
                                break;
                            }
                        }
                        filterAdvertisments(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting advertisment: ' + error.message);
                    });
                }
            });
        };

		function getAdvertismentsById(id) {
            for (var i = 0; i < vm.advertisments.length; i++) {
                var advertisment = vm.advertisments[i];
                if (advertisment.id === id) {
                    return advertisment;
                }
            }
            return null;
        }

        function getAdvertisments() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            advertismentsService.searchAdvertisment(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.advertisments = response.data;
                filterAdvertisments(vm.searchText);
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

    AdvertismentsController.$inject = injectParams;

    app.register.controller('AdvertismentsController', AdvertismentsController);

});
