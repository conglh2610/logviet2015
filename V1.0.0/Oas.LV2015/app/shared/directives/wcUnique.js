'use strict';

define(['app'], function (app) {

    var injectParams = ['$q', '$parse'];

    var wcUniqueDirective = function ($q, $parse) {
        return false;
    };

    wcUniqueDirective.$inject = injectParams;

    app.directive('wcUnique', wcUniqueDirective);

});