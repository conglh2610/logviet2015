'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'languagesService', 'modalService'];

    var LanguagesController = function ($scope, $filter,authService, $location, $window,
        $timeout, languagesService, modalService) {

        var vm = this;
        vm.languages = [];
        vm.filteredLanguages = [];
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
            filterlanguages(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getLanguages();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getlanguages();
        }


        function filterLanguages(filterText) {
            
            vm.filteredLanguages = $filter("languageFilter")(vm.languages, filterText);
            vm.filteredCount = vm.filteredLanguages.length;
        }

		vm.deleteLanguage = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var language = getLanguageById(id);
            var languageName = Language;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Language',
                headerText: 'Delete ' + languageName + '?',
                bodyText: 'Are you sure you want to delete this '+languageName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    languagesService.deleteLanguage(id).then(function () {
                        for (var i = 0; i < vm.languages.length; i++) {
                            if (vm.languages[i].id === id) {
                                vm.languages.splice(i, 1);
                                break;
                            }
							if (vm.filteredLanguages[i].id === id) {
                                vm.filteredLanguages.splice(i, 1);
                                break;
                            }
                        }
                        filterLanguages(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting language: ' + error.message);
                    });
                }
            });
        };

		function getLanguagesById(id) {
            for (var i = 0; i < vm.languages.length; i++) {
                var language = vm.languages[i];
                if (language.id === id) {
                    return language;
                }
            }
            return null;
        }

        function getLanguages() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            languagesService.searchLanguage(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.languages = response.data;
                filterLanguages(vm.searchText);
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

    LanguagesController.$inject = injectParams;

    app.register.controller('LanguagesController', LanguagesController);

});
