'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CarItemsFactory = function ($http, $q) {
        var serviceBase = '/api/carItem';
        var factory = {};

        factory.insertCarItem = function (carItem) {
            return $http.post(serviceBase + "/addCarItem", carItem).then(function (results) {
                CarItem.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCarItem = function (carItem) {
            return $http.post(serviceBase + "/updateCarItem", carItem).then(function (results) {
                CarItem.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCarItem = function (id) {
            return $http.delete(serviceBase + '/deleteCarItem/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCarItem = function (id) {
            return $http.get(serviceBase + '/getCarItemById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCarItem = function (carItemCriteria) {
            return $http.post(serviceBase + "/searchCarItem", carItemCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CarItemsFactory.$inject = injectParams;
    app.factory('carItemsService', CarItemsFactory)
});
