
'use strict';

define(['app'], function (app) {

    var injectParams = ['$http', '$q'];

    var carsService = function ($http, $q) {

        var factory = {};

        var viewBase = '/api/car/';

        factory.getCarsByCriteria = function (criteria) {

            var deferred = $q.defer();

            $http.post(viewBase + 'getCarsByCriteria', criteria).success(function (data, status, headers, config) {

                var results = [];

                results.data = data;
                results.headers = headers();
                results.status = status;
                results.config = config;

                deferred.resolve(results);

            }).error(deferred.reject);

            return deferred.promise;
        }

        factory.getCars = function () {

            var deferred = $q.defer();

            $http.get(viewBase + 'getCars').success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        factory.getCar = function (carId) {
            var deferred = $q.defer();
            $http.get(viewBase + 'getCar' + '/' + carId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.addCar = function (car) {
            var deferred = $q.defer();
            $http.post(viewBase + 'addCar', car).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.updateCar = function (car) {
            var deferred = $q.defer();
            $http.put(viewBase + 'updateCar', car).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.deleteCar = function (carId) {
            var deferred = $q.defer();
            $http.delete(viewBase + 'deleteCar/' + carId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        return factory;
    }

    carsService.$inject = injectParams;

    app.factory('carsService', carsService);

});