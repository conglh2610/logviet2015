
'use strict';

define(['app'], function (app) {

    var injectParams = ['$http', '$q'];

    var driversService = function ($http, $q) {

        var factory = {};

        var viewBase = '/api/driver/';

        factory.getDriverByCriteria = function (criteria) {

            var deferred = $q.defer();

            $http.post(viewBase + 'getDriverByCriteria', criteria).success(function (data, status, headers, config) {

                var results = [];

                results.data = data;
                results.headers = headers();
                results.status = status;
                results.config = config;

                deferred.resolve(results);

            }).error(deferred.reject);

            return deferred.promise;
        }

        factory.getDriver = function () {

            var deferred = $q.defer();

            $http.get(viewBase + 'getDriver').success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }


        factory.search = function (criteria) {

            var deferred = $q.defer();

            $http.post(viewBase + 'searchdriver/', criteria).success(function (data, status, headers, config) {

                var results = [];

                results.data = data;
                results.headers = headers();
                results.status = status;
                results.config = config;

                deferred.resolve(results);

            }).error(deferred.reject);

            return deferred.promise;
        }

        factory.getDriver = function (driverId) {
            var deferred = $q.defer();
            $http.get(viewBase + 'getDriverById' + '/' + driverId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.addDriver = function (driver) {
            var deferred = $q.defer();
            $http.post(viewBase + 'addDriver', driver).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.checkUniqueDriver = function (categoryName) {
            var deferred = $q.defer();
            $http.get(viewBase + 'checkUniqueDriver?DriverName=' + categoryName).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.updateDriver = function (driver) {
            var deferred = $q.defer();
            $http.put(viewBase + 'updateDriver', driver).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.deleteDriver = function (driverId) {
            var deferred = $q.defer();
            $http.delete(viewBase + 'deleteDriver/' + driverId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        return factory;
    }

    driversService.$inject = injectParams;

    app.factory('driversService', driversService);

});