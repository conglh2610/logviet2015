'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var BusinessPromotionsFactory = function ($http, $q) {
        var serviceBase = '/api/businessPromotion';
        var factory = {};

        factory.insertBusinessPromotion = function (businessPromotion) {
            return $http.post(serviceBase + "/addBusinessPromotion", businessPromotion).then(function (results) {
                BusinessPromotion.id = results.data.id;
                return results.data;
            });
        }

        factory.updateBusinessPromotion = function (businessPromotion) {
            return $http.post(serviceBase + "/updateBusinessPromotion", businessPromotion).then(function (results) {
                BusinessPromotion.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteBusinessPromotion = function (id) {
            return $http.delete(serviceBase + '/deleteBusinessPromotion/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getBusinessPromotion = function (id) {
            return $http.get(serviceBase + '/getBusinessPromotionById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchBusinessPromotion = function (businessPromotionCriteria) {
            return $http.post(serviceBase + "/searchBusinessPromotion", businessPromotionCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    BusinessPromotionsFactory.$inject = injectParams;
    app.factory('businessPromotionsService', BusinessPromotionsFactory)
});
