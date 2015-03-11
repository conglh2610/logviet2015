'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CarsFactory = function ($http, $q) {
        var serviceBase = '/api/car';
        var factory = {};

        factory.insertCar = function (car) {
            return $http.post(serviceBase + "/addCar", car).then(function (results) {
                Car.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCar = function (car) {
            return $http.post(serviceBase + "/updateCar", car).then(function (results) {
                Car.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCar = function (id) {
            return $http.delete(serviceBase + '/deleteCar/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCar = function (id) {
            return $http.get(serviceBase + '/getCarById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCar = function (carCriteria) {
            return $http.post(serviceBase + "/searchCar", carCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CarsFactory.$inject = injectParams;
    app.factory('carsService', CarsFactory)
});
