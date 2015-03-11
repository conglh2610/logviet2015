'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'emailTemplatesService', 'modalService'];

    var EmailTemplatesController = function ($scope, $filter,authService, $location, $window,
        $timeout, emailTemplatesService, modalService) {

        var vm = this;
        vm.emailTemplates = [];
        vm.filteredEmailTemplates = [];
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
            filteremailTemplates(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getEmailTemplates();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getemailTemplates();
        }


        function filterEmailTemplates(filterText) {
            
            vm.filteredEmailTemplates = $filter("emailTemplateFilter")(vm.emailTemplates, filterText);
            vm.filteredCount = vm.filteredEmailTemplates.length;
        }

		vm.deleteEmailTemplate = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var emailTemplate = getEmailTemplateById(id);
            var emailTemplateName = EmailTemplate;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete EmailTemplate',
                headerText: 'Delete ' + emailTemplateName + '?',
                bodyText: 'Are you sure you want to delete this '+emailTemplateName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    emailTemplatesService.deleteEmailTemplate(id).then(function () {
                        for (var i = 0; i < vm.emailTemplates.length; i++) {
                            if (vm.emailTemplates[i].id === id) {
                                vm.emailTemplates.splice(i, 1);
                                break;
                            }
							if (vm.filteredEmailTemplates[i].id === id) {
                                vm.filteredEmailTemplates.splice(i, 1);
                                break;
                            }
                        }
                        filterEmailTemplates(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting emailTemplate: ' + error.message);
                    });
                }
            });
        };

		function getEmailTemplatesById(id) {
            for (var i = 0; i < vm.emailTemplates.length; i++) {
                var emailTemplate = vm.emailTemplates[i];
                if (emailTemplate.id === id) {
                    return emailTemplate;
                }
            }
            return null;
        }

        function getEmailTemplates() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            emailTemplatesService.searchEmailTemplate(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.emailTemplates = response.data;
                filterEmailTemplates(vm.searchText);
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

    EmailTemplatesController.$inject = injectParams;

    app.register.controller('EmailTemplatesController', EmailTemplatesController);

});
