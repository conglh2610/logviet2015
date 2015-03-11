'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'studyTimesService', 'modalService'];

    var StudyTimesController = function ($scope, $filter,authService, $location, $window,
        $timeout, studyTimesService, modalService) {

        var vm = this;
        vm.studyTimes = [];
        vm.filteredStudyTimes = [];
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
            filterstudyTimes(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getStudyTimes();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getstudyTimes();
        }


        function filterStudyTimes(filterText) {
            
            vm.filteredStudyTimes = $filter("studyTimeFilter")(vm.studyTimes, filterText);
            vm.filteredCount = vm.filteredStudyTimes.length;
        }

		vm.deleteStudyTime = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var studyTime = getStudyTimeById(id);
            var studyTimeName = StudyTime;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete StudyTime',
                headerText: 'Delete ' + studyTimeName + '?',
                bodyText: 'Are you sure you want to delete this '+studyTimeName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    studyTimesService.deleteStudyTime(id).then(function () {
                        for (var i = 0; i < vm.studyTimes.length; i++) {
                            if (vm.studyTimes[i].id === id) {
                                vm.studyTimes.splice(i, 1);
                                break;
                            }
							if (vm.filteredStudyTimes[i].id === id) {
                                vm.filteredStudyTimes.splice(i, 1);
                                break;
                            }
                        }
                        filterStudyTimes(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting studyTime: ' + error.message);
                    });
                }
            });
        };

		function getStudyTimesById(id) {
            for (var i = 0; i < vm.studyTimes.length; i++) {
                var studyTime = vm.studyTimes[i];
                if (studyTime.id === id) {
                    return studyTime;
                }
            }
            return null;
        }

        function getStudyTimes() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            studyTimesService.searchStudyTime(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.studyTimes = response.data;
                filterStudyTimes(vm.searchText);
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

    StudyTimesController.$inject = injectParams;

    app.register.controller('StudyTimesController', StudyTimesController);

});
