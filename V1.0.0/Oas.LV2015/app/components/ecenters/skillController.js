'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$window',
                        '$timeout', 'studentsService', 'modalService'];

    var SkillController = function ($scope, $location, $window,
        $timeout, studentsService, modalService) {


        var vm = this;
        vm.title = "Add a";
        vm.buttonText = "Add";
    };

    SkillController.$inject = injectParams;

    app.register.controller("skillController", SkillController);
    
});