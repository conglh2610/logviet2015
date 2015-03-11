'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window',
                       '$timeout', '$modal', 'studentsService', 'modalService'];

    var ListMaintenanceController = function ($scope, $location, $window,
        $timeout, $modal, studentsService, modalService) {

        var vm = this;

        vm.addSkill = function () {

            var modalInstance = $modal.open({
                templateUrl: 'app/components/ecenters/skillItem.html',
                controller: 'skillController',
                size: "sm",
                resolve: {
                    
                }
            });

        }

    }

    ListMaintenanceController.$inject = injectParams;
    app.register.controller("listmaintenanceController", ListMaintenanceController);
});