'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ResultsFactory = function ($http, $q) {
        var serviceBase = '/api/result';
        var factory = {};

        factory.insertResult = function (result) {
            return $http.post(serviceBase + "/addResult", result).then(function (results) {
                Result.id = results.data.id;
                return results.data;
            });
        }

        factory.updateResult = function (result) {
            return $http.post(serviceBase + "/updateResult", result).then(function (results) {
                Result.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteResult = function (id) {
            return $http.delete(serviceBase + '/deleteResult/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getResult = function (id) {
            return $http.get(serviceBase + '/getResultById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchResult = function (resultCriteria) {
            return $http.post(serviceBase + "/searchResult", resultCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ResultsFactory.$inject = injectParams;
    app.factory('resultsService', ResultsFactory)
});
