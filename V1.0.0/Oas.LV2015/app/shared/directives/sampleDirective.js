'use strict';

define(['app'], function (app) {

    var injectParams = ['$q', '$parse'];

    var sampleDirective = function ($q, $parse) {


        return false;
    };

    sampleDirective.$inject = injectParams;

    app.directive('KhanhCompile', sampleDirective);

});