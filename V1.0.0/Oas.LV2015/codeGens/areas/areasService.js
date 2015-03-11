'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var AreasFactory = function ($http, $q) {
        var serviceBase = '/api/area';
        var factory = {};

        factory.insertArea = function (area) {
            return $http.post(serviceBase + "/addArea", area).then(function (results) {
                Area.id = results.data.id;
                return results.data;
            });
        }

        factory.updateArea = function (area) {
            return $http.post(serviceBase + "/updateArea", area).then(function (results) {
                Area.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteArea = function (id) {
            return $http.delete(serviceBase + '/deleteArea/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getArea = function (id) {
            return $http.get(serviceBase + '/getAreaById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchArea = function (areaCriteria) {
            return $http.post(serviceBase + "/searchArea", areaCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    AreasFactory.$inject = injectParams;
    app.factory('areasService', AreasFactory)
});
