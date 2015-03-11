'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CarBookingsFactory = function ($http, $q) {
        var serviceBase = '/api/carBooking';
        var factory = {};

        factory.insertCarBooking = function (carBooking) {
            return $http.post(serviceBase + "/addCarBooking", carBooking).then(function (results) {
                CarBooking.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCarBooking = function (carBooking) {
            return $http.post(serviceBase + "/updateCarBooking", carBooking).then(function (results) {
                CarBooking.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCarBooking = function (id) {
            return $http.delete(serviceBase + '/deleteCarBooking/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCarBooking = function (id) {
            return $http.get(serviceBase + '/getCarBookingById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCarBooking = function (carBookingCriteria) {
            return $http.post(serviceBase + "/searchCarBooking", carBookingCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CarBookingsFactory.$inject = injectParams;
    app.factory('carBookingsService', CarBookingsFactory)
});
