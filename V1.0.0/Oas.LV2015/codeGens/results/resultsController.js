'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'resultsService', 'modalService'];

    var ResultsController = function ($scope, $filter,authService, $location, $window,
        $timeout, resultsService, modalService) {

        var vm = this;
        vm.results = [];
        vm.filteredResults = [];
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
            filterresults(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getResults();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getresults();
        }


        function filterResults(filterText) {
            
            vm.filteredResults = $filter("resultFilter")(vm.results, filterText);
            vm.filteredCount = vm.filteredResults.length;
        }

		vm.deleteResult = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var result = getResultById(id);
            var resultName = Result;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Result',
                headerText: 'Delete ' + resultName + '?',
                bodyText: 'Are you sure you want to delete this '+resultName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    resultsService.deleteResult(id).then(function () {
                        for (var i = 0; i < vm.results.length; i++) {
                            if (vm.results[i].id === id) {
                                vm.results.splice(i, 1);
                                break;
                            }
							if (vm.filteredResults[i].id === id) {
                                vm.filteredResults.splice(i, 1);
                                break;
                            }
                        }
                        filterResults(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting result: ' + error.message);
                    });
                }
            });
        };

		function getResultsById(id) {
            for (var i = 0; i < vm.results.length; i++) {
                var result = vm.results[i];
                if (result.id === id) {
                    return result;
                }
            }
            return null;
        }

        function getResults() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            resultsService.searchResult(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.results = response.data;
                filterResults(vm.searchText);
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

    ResultsController.$inject = injectParams;

    app.register.controller('ResultsController', ResultsController);

});
