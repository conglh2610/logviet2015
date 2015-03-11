'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var AdvertismentsFactory = function ($http, $q) {
        var serviceBase = '/api/advertisment';
        var factory = {};

        factory.insertAdvertisment = function (advertisment) {
            return $http.post(serviceBase + "/addAdvertisment", advertisment).then(function (results) {
                Advertisment.id = results.data.id;
                return results.data;
            });
        }

        factory.updateAdvertisment = function (advertisment) {
            return $http.post(serviceBase + "/updateAdvertisment", advertisment).then(function (results) {
                Advertisment.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteAdvertisment = function (id) {
            return $http.delete(serviceBase + '/deleteAdvertisment/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getAdvertisment = function (id) {
            return $http.get(serviceBase + '/getAdvertismentById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchAdvertisment = function (advertismentCriteria) {
            return $http.post(serviceBase + "/searchAdvertisment", advertismentCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    AdvertismentsFactory.$inject = injectParams;
    app.factory('advertismentsService', AdvertismentsFactory)
});
