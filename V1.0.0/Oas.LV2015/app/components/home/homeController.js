'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope','$location', '$filter', '$window',
                        '$timeout', 'authService', 'modalService'];

    var HomeController = function ($scope,$location, $filter, $window,
        $timeout, authService, modalService) {

        var vm = this;

        angular.extend($scope, {
            centerProperty: {
                lat: 16,
                lng: 108.2
            },
            zoomProperty: 12,
            markersProperty: [{
                latitude: 16,
                longitude: 108.2
            }],
            clickedLatitudeProperty: null,
            clickedLongitudeProperty: null,
        });

    };
    
    HomeController.$inject = injectParams;

    app.register.controller('HomeController', HomeController);

});