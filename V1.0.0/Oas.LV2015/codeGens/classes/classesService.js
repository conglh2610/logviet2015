'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ClassesFactory = function ($http, $q) {
        var serviceBase = '/api/class';
        var factory = {};

        factory.insertClass = function (class) {
            return $http.post(serviceBase + "/addClass", class).then(function (results) {
                Class.id = results.data.id;
                return results.data;
            });
        }

        factory.updateClass = function (class) {
            return $http.post(serviceBase + "/updateClass", class).then(function (results) {
                Class.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteClass = function (id) {
            return $http.delete(serviceBase + '/deleteClass/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getClass = function (id) {
            return $http.get(serviceBase + '/getClassById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchClass = function (classCriteria) {
            return $http.post(serviceBase + "/searchClass", classCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ClassesFactory.$inject = injectParams;
    app.factory('classesService', ClassesFactory)
});
