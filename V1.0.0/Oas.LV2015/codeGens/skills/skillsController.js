'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'skillsService', 'modalService'];

    var SkillsController = function ($scope, $filter,authService, $location, $window,
        $timeout, skillsService, modalService) {

        var vm = this;
        vm.skills = [];
        vm.filteredSkills = [];
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
            filterskills(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getSkills();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getskills();
        }


        function filterSkills(filterText) {
            
            vm.filteredSkills = $filter("skillFilter")(vm.skills, filterText);
            vm.filteredCount = vm.filteredSkills.length;
        }

		vm.deleteSkill = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var skill = getSkillById(id);
            var skillName = Skill;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Skill',
                headerText: 'Delete ' + skillName + '?',
                bodyText: 'Are you sure you want to delete this '+skillName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    skillsService.deleteSkill(id).then(function () {
                        for (var i = 0; i < vm.skills.length; i++) {
                            if (vm.skills[i].id === id) {
                                vm.skills.splice(i, 1);
                                break;
                            }
							if (vm.filteredSkills[i].id === id) {
                                vm.filteredSkills.splice(i, 1);
                                break;
                            }
                        }
                        filterSkills(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting skill: ' + error.message);
                    });
                }
            });
        };

		function getSkillsById(id) {
            for (var i = 0; i < vm.skills.length; i++) {
                var skill = vm.skills[i];
                if (skill.id === id) {
                    return skill;
                }
            }
            return null;
        }

        function getSkills() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            skillsService.searchSkill(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.skills = response.data;
                filterSkills(vm.searchText);
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

    SkillsController.$inject = injectParams;

    app.register.controller('SkillsController', SkillsController);

});
