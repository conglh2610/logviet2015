'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CarAccidentsFactory = function ($http, $q) {
        var serviceBase = '/api/carAccident';
        var factory = {};

        factory.insertCarAccident = function (carAccident) {
            return $http.post(serviceBase + "/addCarAccident", carAccident).then(function (results) {
                CarAccident.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCarAccident = function (carAccident) {
            return $http.post(serviceBase + "/updateCarAccident", carAccident).then(function (results) {
                CarAccident.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCarAccident = function (id) {
            return $http.delete(serviceBase + '/deleteCarAccident/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCarAccident = function (id) {
            return $http.get(serviceBase + '/getCarAccidentById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCarAccident = function (carAccidentCriteria) {
            return $http.post(serviceBase + "/searchCarAccident", carAccidentCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CarAccidentsFactory.$inject = injectParams;
    app.factory('carAccidentsService', CarAccidentsFactory)
});
