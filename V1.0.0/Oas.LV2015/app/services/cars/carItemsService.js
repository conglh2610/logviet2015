
'use strict';

define(['app'], function (app) {

    var injectParams = ['$http', '$q'];

    var carItemsService = function ($http, $q) {

        var factory = {};

        var viewBase = '/api/carItem/';

        factory.getCarItemsByCriteria = function (criteria) {

            var deferred = $q.defer();

            $http.post(viewBase + 'getCarItemsByCriteria', criteria).success(function (data, status, headers, config) {

                var results = [];

                results.data = data;
                results.headers = headers();
                results.status = status;
                results.config = config;

                deferred.resolve(results);

            }).error(deferred.reject);

            return deferred.promise;
        }

        factory.getCarItems = function () {

            var deferred = $q.defer();

            $http.get(viewBase + 'getCarItems').success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        factory.getCarItem = function (carItemId) {
            var deferred = $q.defer();
            $http.get(viewBase + 'getCarItem' + '/' + carItemId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.addCarItem = function (carItem) {
            var deferred = $q.defer();
            $http.post(viewBase + 'addCarItem', carItem).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.updateCarItem = function (carItem) {
            var deferred = $q.defer();
            $http.put(viewBase + 'updateCarItem', carItem).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.deleteCarItem = function (carItemId) {
            var deferred = $q.defer();
            $http.delete(viewBase + 'deleteCarItem/' + carItemId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        return factory;
    }

    carItemsService.$inject = injectParams;

    app.factory('carItemsService', carItemsService);

});