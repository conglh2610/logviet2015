'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'businessPromotionsService', 'modalService'];

    var BusinessPromotionsController = function ($scope, $filter,authService, $location, $window,
        $timeout, businessPromotionsService, modalService) {

        var vm = this;
        vm.businessPromotions = [];
        vm.filteredBusinessPromotions = [];
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
            filterbusinessPromotions(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getBusinessPromotions();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getbusinessPromotions();
        }


        function filterBusinessPromotions(filterText) {
            
            vm.filteredBusinessPromotions = $filter("businessPromotionFilter")(vm.businessPromotions, filterText);
            vm.filteredCount = vm.filteredBusinessPromotions.length;
        }

		vm.deleteBusinessPromotion = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var businessPromotion = getBusinessPromotionById(id);
            var businessPromotionName = BusinessPromotion;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete BusinessPromotion',
                headerText: 'Delete ' + businessPromotionName + '?',
                bodyText: 'Are you sure you want to delete this '+businessPromotionName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    businessPromotionsService.deleteBusinessPromotion(id).then(function () {
                        for (var i = 0; i < vm.businessPromotions.length; i++) {
                            if (vm.businessPromotions[i].id === id) {
                                vm.businessPromotions.splice(i, 1);
                                break;
                            }
							if (vm.filteredBusinessPromotions[i].id === id) {
                                vm.filteredBusinessPromotions.splice(i, 1);
                                break;
                            }
                        }
                        filterBusinessPromotions(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting businessPromotion: ' + error.message);
                    });
                }
            });
        };

		function getBusinessPromotionsById(id) {
            for (var i = 0; i < vm.businessPromotions.length; i++) {
                var businessPromotion = vm.businessPromotions[i];
                if (businessPromotion.id === id) {
                    return businessPromotion;
                }
            }
            return null;
        }

        function getBusinessPromotions() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            businessPromotionsService.searchBusinessPromotion(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.businessPromotions = response.data;
                filterBusinessPromotions(vm.searchText);
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

    BusinessPromotionsController.$inject = injectParams;

    app.register.controller('BusinessPromotionsController', BusinessPromotionsController);

});
