'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var MessageHistoriesFactory = function ($http, $q) {
        var serviceBase = '/api/messageHistory';
        var factory = {};

        factory.insertMessageHistory = function (messageHistory) {
            return $http.post(serviceBase + "/addMessageHistory", messageHistory).then(function (results) {
                MessageHistory.id = results.data.id;
                return results.data;
            });
        }

        factory.updateMessageHistory = function (messageHistory) {
            return $http.post(serviceBase + "/updateMessageHistory", messageHistory).then(function (results) {
                MessageHistory.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteMessageHistory = function (id) {
            return $http.delete(serviceBase + '/deleteMessageHistory/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getMessageHistory = function (id) {
            return $http.get(serviceBase + '/getMessageHistoryById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchMessageHistory = function (messageHistoryCriteria) {
            return $http.post(serviceBase + "/searchMessageHistory", messageHistoryCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    MessageHistoriesFactory.$inject = injectParams;
    app.factory('messageHistoriesService', MessageHistoriesFactory)
});
