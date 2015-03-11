'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'membershipPackagesService', 'modalService'];

    var MembershipPackagesController = function ($scope, $filter,authService, $location, $window,
        $timeout, membershipPackagesService, modalService) {

        var vm = this;
        vm.membershipPackages = [];
        vm.filteredMembershipPackages = [];
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
            filtermembershipPackages(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getMembershipPackages();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getmembershipPackages();
        }


        function filterMembershipPackages(filterText) {
            
            vm.filteredMembershipPackages = $filter("membershipPackageFilter")(vm.membershipPackages, filterText);
            vm.filteredCount = vm.filteredMembershipPackages.length;
        }

		vm.deleteMembershipPackage = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var membershipPackage = getMembershipPackageById(id);
            var membershipPackageName = MembershipPackage;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete MembershipPackage',
                headerText: 'Delete ' + membershipPackageName + '?',
                bodyText: 'Are you sure you want to delete this '+membershipPackageName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    membershipPackagesService.deleteMembershipPackage(id).then(function () {
                        for (var i = 0; i < vm.membershipPackages.length; i++) {
                            if (vm.membershipPackages[i].id === id) {
                                vm.membershipPackages.splice(i, 1);
                                break;
                            }
							if (vm.filteredMembershipPackages[i].id === id) {
                                vm.filteredMembershipPackages.splice(i, 1);
                                break;
                            }
                        }
                        filterMembershipPackages(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting membershipPackage: ' + error.message);
                    });
                }
            });
        };

		function getMembershipPackagesById(id) {
            for (var i = 0; i < vm.membershipPackages.length; i++) {
                var membershipPackage = vm.membershipPackages[i];
                if (membershipPackage.id === id) {
                    return membershipPackage;
                }
            }
            return null;
        }

        function getMembershipPackages() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            membershipPackagesService.searchMembershipPackage(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.membershipPackages = response.data;
                filterMembershipPackages(vm.searchText);
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

    MembershipPackagesController.$inject = injectParams;

    app.register.controller('MembershipPackagesController', MembershipPackagesController);

});
