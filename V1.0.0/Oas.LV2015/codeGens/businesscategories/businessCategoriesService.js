'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var BusinessCategoriesFactory = function ($http, $q) {
        var serviceBase = '/api/businessCategory';
        var factory = {};

        factory.insertBusinessCategory = function (businessCategory) {
            return $http.post(serviceBase + "/addBusinessCategory", businessCategory).then(function (results) {
                BusinessCategory.id = results.data.id;
                return results.data;
            });
        }

        factory.updateBusinessCategory = function (businessCategory) {
            return $http.post(serviceBase + "/updateBusinessCategory", businessCategory).then(function (results) {
                BusinessCategory.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteBusinessCategory = function (id) {
            return $http.delete(serviceBase + '/deleteBusinessCategory/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getBusinessCategory = function (id) {
            return $http.get(serviceBase + '/getBusinessCategoryById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchBusinessCategory = function (businessCategoryCriteria) {
            return $http.post(serviceBase + "/searchBusinessCategory", businessCategoryCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    BusinessCategoriesFactory.$inject = injectParams;
    app.factory('businessCategoriesService', BusinessCategoriesFactory)
});
