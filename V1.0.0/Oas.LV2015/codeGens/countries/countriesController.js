'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'countriesService', 'modalService'];

    var CountriesController = function ($scope, $filter,authService, $location, $window,
        $timeout, countriesService, modalService) {

        var vm = this;
        vm.countries = [];
        vm.filteredCountries = [];
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
            filtercountries(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getCountries();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getcountries();
        }


        function filterCountries(filterText) {
            
            vm.filteredCountries = $filter("countryFilter")(vm.countries, filterText);
            vm.filteredCount = vm.filteredCountries.length;
        }

		vm.deleteCountry = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var country = getCountryById(id);
            var countryName = Country;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Country',
                headerText: 'Delete ' + countryName + '?',
                bodyText: 'Are you sure you want to delete this '+countryName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    countriesService.deleteCountry(id).then(function () {
                        for (var i = 0; i < vm.countries.length; i++) {
                            if (vm.countries[i].id === id) {
                                vm.countries.splice(i, 1);
                                break;
                            }
							if (vm.filteredCountries[i].id === id) {
                                vm.filteredCountries.splice(i, 1);
                                break;
                            }
                        }
                        filterCountries(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting country: ' + error.message);
                    });
                }
            });
        };

		function getCountriesById(id) {
            for (var i = 0; i < vm.countries.length; i++) {
                var country = vm.countries[i];
                if (country.id === id) {
                    return country;
                }
            }
            return null;
        }

        function getCountries() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            countriesService.searchCountry(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.countries = response.data;
                filterCountries(vm.searchText);
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

    CountriesController.$inject = injectParams;

    app.register.controller('CountriesController', CountriesController);

});
