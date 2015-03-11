'use strict';

define(['app'], function (app) {

    var injectParams = ['$location', '$filter', '$window',
                        '$timeout', 'authService', 'modalService'];

    var EcentersController = function ($location, $filter, $window,
        $timeout, authService, modalService) {

        var vm = this;

    };

    EcentersController.$inject = injectParams;

    app.register.controller('EcentersController', EcentersController);

});