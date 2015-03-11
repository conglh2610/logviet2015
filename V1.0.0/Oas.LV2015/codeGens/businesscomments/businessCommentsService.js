'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var BusinessCommentsFactory = function ($http, $q) {
        var serviceBase = '/api/businessComment';
        var factory = {};

        factory.insertBusinessComment = function (businessComment) {
            return $http.post(serviceBase + "/addBusinessComment", businessComment).then(function (results) {
                BusinessComment.id = results.data.id;
                return results.data;
            });
        }

        factory.updateBusinessComment = function (businessComment) {
            return $http.post(serviceBase + "/updateBusinessComment", businessComment).then(function (results) {
                BusinessComment.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteBusinessComment = function (id) {
            return $http.delete(serviceBase + '/deleteBusinessComment/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getBusinessComment = function (id) {
            return $http.get(serviceBase + '/getBusinessCommentById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchBusinessComment = function (businessCommentCriteria) {
            return $http.post(serviceBase + "/searchBusinessComment", businessCommentCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    BusinessCommentsFactory.$inject = injectParams;
    app.factory('businessCommentsService', BusinessCommentsFactory)
});
