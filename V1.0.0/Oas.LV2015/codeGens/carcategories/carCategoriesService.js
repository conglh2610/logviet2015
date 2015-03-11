'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CarCategoriesFactory = function ($http, $q) {
        var serviceBase = '/api/carCategory';
        var factory = {};

        factory.insertCarCategory = function (carCategory) {
            return $http.post(serviceBase + "/addCarCategory", carCategory).then(function (results) {
                CarCategory.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCarCategory = function (carCategory) {
            return $http.post(serviceBase + "/updateCarCategory", carCategory).then(function (results) {
                CarCategory.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCarCategory = function (id) {
            return $http.delete(serviceBase + '/deleteCarCategory/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCarCategory = function (id) {
            return $http.get(serviceBase + '/getCarCategoryById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCarCategory = function (carCategoryCriteria) {
            return $http.post(serviceBase + "/searchCarCategory", carCategoryCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CarCategoriesFactory.$inject = injectParams;
    app.factory('carCategoriesService', CarCategoriesFactory)
});
