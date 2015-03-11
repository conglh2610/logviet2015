'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CarModelsFactory = function ($http, $q) {
        var serviceBase = '/api/carModel';
        var factory = {};

        factory.insertCarModel = function (carModel) {
            return $http.post(serviceBase + "/addCarModel", carModel).then(function (results) {
                CarModel.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCarModel = function (carModel) {
            return $http.post(serviceBase + "/updateCarModel", carModel).then(function (results) {
                CarModel.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCarModel = function (id) {
            return $http.delete(serviceBase + '/deleteCarModel/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCarModel = function (id) {
            return $http.get(serviceBase + '/getCarModelById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCarModel = function (carModelCriteria) {
            return $http.post(serviceBase + "/searchCarModel", carModelCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CarModelsFactory.$inject = injectParams;
    app.factory('carModelsService', CarModelsFactory)
});
