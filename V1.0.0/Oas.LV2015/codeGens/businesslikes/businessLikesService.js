'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var BusinessLikesFactory = function ($http, $q) {
        var serviceBase = '/api/businessLike';
        var factory = {};

        factory.insertBusinessLike = function (businessLike) {
            return $http.post(serviceBase + "/addBusinessLike", businessLike).then(function (results) {
                BusinessLike.id = results.data.id;
                return results.data;
            });
        }

        factory.updateBusinessLike = function (businessLike) {
            return $http.post(serviceBase + "/updateBusinessLike", businessLike).then(function (results) {
                BusinessLike.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteBusinessLike = function (id) {
            return $http.delete(serviceBase + '/deleteBusinessLike/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getBusinessLike = function (id) {
            return $http.get(serviceBase + '/getBusinessLikeById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchBusinessLike = function (businessLikeCriteria) {
            return $http.post(serviceBase + "/searchBusinessLike", businessLikeCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    BusinessLikesFactory.$inject = injectParams;
    app.factory('businessLikesService', BusinessLikesFactory)
});
