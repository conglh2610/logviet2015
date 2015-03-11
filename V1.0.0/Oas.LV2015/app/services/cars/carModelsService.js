
'use strict';

define(['app'], function (app) {

    var injectParams = ['$http', '$q'];

    var carModelsService = function ($http, $q) {

        var factory = {};

        var viewBase = '/api/carModel/';

        factory.getCarModelsByCriteria = function (criteria) {

            var deferred = $q.defer();

            $http.post(viewBase + 'getCarModelsByCriteria', criteria).success(function (data, status, headers, config) {

                var results = [];

                results.data = data;
                results.headers = headers();
                results.status = status;
                results.config = config;

                deferred.resolve(results);
            }).error(deferred.reject);

            return deferred.promise;
        }

        factory.getCarModels = function () {

            var deferred = $q.defer();

            $http.get(viewBase + 'getCarModels').success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        factory.getCarModel = function (carModelId) {
            var deferred = $q.defer();
            $http.get(viewBase + 'getCarModel' + '/' + carModelId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.addCarModel = function (carModel) {
            var deferred = $q.defer();
            $http.post(viewBase + 'addCarModel', carModel).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.checkUniqueCarModel = function (categoryName) {
            var deferred = $q.defer();
            $http.get(viewBase + 'checkUniqueCarModel?CarModelName=' + categoryName).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.updateCarModel = function (carModel) {
            var deferred = $q.defer();
            $http.put(viewBase + 'updateCarModel', carModel).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.deleteCarModel = function (carModelId) {
            var deferred = $q.defer();
            $http.delete(viewBase + 'deleteCarModel/' + carModelId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        return factory;
    }

    carModelsService.$inject = injectParams;

    app.factory('carModelsService', carModelsService);

});