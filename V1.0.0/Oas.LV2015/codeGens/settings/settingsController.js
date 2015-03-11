'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'settingsService', 'modalService'];

    var SettingsController = function ($scope, $filter,authService, $location, $window,
        $timeout, settingsService, modalService) {

        var vm = this;
        vm.settings = [];
        vm.filteredSettings = [];
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
            filtersettings(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getSettings();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getsettings();
        }


        function filterSettings(filterText) {
            
            vm.filteredSettings = $filter("settingFilter")(vm.settings, filterText);
            vm.filteredCount = vm.filteredSettings.length;
        }

		vm.deleteSetting = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var setting = getSettingById(id);
            var settingName = Setting;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Setting',
                headerText: 'Delete ' + settingName + '?',
                bodyText: 'Are you sure you want to delete this '+settingName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    settingsService.deleteSetting(id).then(function () {
                        for (var i = 0; i < vm.settings.length; i++) {
                            if (vm.settings[i].id === id) {
                                vm.settings.splice(i, 1);
                                break;
                            }
							if (vm.filteredSettings[i].id === id) {
                                vm.filteredSettings.splice(i, 1);
                                break;
                            }
                        }
                        filterSettings(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting setting: ' + error.message);
                    });
                }
            });
        };

		function getSettingsById(id) {
            for (var i = 0; i < vm.settings.length; i++) {
                var setting = vm.settings[i];
                if (setting.id === id) {
                    return setting;
                }
            }
            return null;
        }

        function getSettings() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            settingsService.searchSetting(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.settings = response.data;
                filterSettings(vm.searchText);
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

    SettingsController.$inject = injectParams;

    app.register.controller('SettingsController', SettingsController);

});
