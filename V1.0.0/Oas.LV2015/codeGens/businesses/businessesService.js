'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var BusinessesFactory = function ($http, $q) {
        var serviceBase = '/api/business';
        var factory = {};

        factory.insertBusiness = function (business) {
            return $http.post(serviceBase + "/addBusiness", business).then(function (results) {
                Business.id = results.data.id;
                return results.data;
            });
        }

        factory.updateBusiness = function (business) {
            return $http.post(serviceBase + "/updateBusiness", business).then(function (results) {
                Business.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteBusiness = function (id) {
            return $http.delete(serviceBase + '/deleteBusiness/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getBusiness = function (id) {
            return $http.get(serviceBase + '/getBusinessById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchBusiness = function (businessCriteria) {
            return $http.post(serviceBase + "/searchBusiness", businessCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    BusinessesFactory.$inject = injectParams;
    app.factory('businessesService', BusinessesFactory)
});
