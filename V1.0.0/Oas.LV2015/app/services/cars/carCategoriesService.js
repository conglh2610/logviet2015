
'use strict';

define(['app'], function (app) {

    var injectParams = ['$http', '$q'];

    var carCategoriesService = function ($http, $q) {

        var factory = {};

        var viewBase = '/api/carCategory/';

        factory.getCarCategoriesByCriteria = function (criteria) {

            var deferred = $q.defer();

            $http.post(viewBase + 'getCarCategoriesByCriteria', criteria).success(function (data, status, headers, config) {

                var results = [];

                results.data = data;
                results.headers = headers();
                results.status = status;
                results.config = config;

                deferred.resolve(results);
            }).error(deferred.reject);

            return deferred.promise;
        }

        factory.getCarCategories = function () {
            var deferred = $q.defer();
            $http.post(viewBase + 'getCarCategories').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.getCarCategory = function (carCategoryId) {
            var deferred = $q.defer();
            $http.get(viewBase + 'getCarCategory' + '/' + carCategoryId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.addCarCategory = function (carCategory) {
            var deferred = $q.defer();
            $http.post(viewBase + 'addCarCategory', carCategory).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.updateCarCategory = function (carCategory) {
            var deferred = $q.defer();
            $http.put(viewBase + 'updateCarCategory', carCategory).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        factory.deleteCarCategory = function (carCategoryId) {
            var deferred = $q.defer();
            $http.delete(viewBase + 'deleteCarCategory/' + carCategoryId).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        return factory;
    }

    carCategoriesService.$inject = injectParams;

    app.factory('carCategoriesService', carCategoriesService);

});