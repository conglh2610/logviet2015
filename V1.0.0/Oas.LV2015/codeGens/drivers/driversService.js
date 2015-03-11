'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var DriversFactory = function ($http, $q) {
        var serviceBase = '/api/driver';
        var factory = {};

        factory.insertDriver = function (driver) {
            return $http.post(serviceBase + "/addDriver", driver).then(function (results) {
                Driver.id = results.data.id;
                return results.data;
            });
        }

        factory.updateDriver = function (driver) {
            return $http.post(serviceBase + "/updateDriver", driver).then(function (results) {
                Driver.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteDriver = function (id) {
            return $http.delete(serviceBase + '/deleteDriver/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getDriver = function (id) {
            return $http.get(serviceBase + '/getDriverById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchDriver = function (driverCriteria) {
            return $http.post(serviceBase + "/searchDriver", driverCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    DriversFactory.$inject = injectParams;
    app.factory('driversService', DriversFactory)
});
