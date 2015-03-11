'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var UserApplicationsFactory = function ($http, $q) {
        var serviceBase = '/api/userApplication';
        var factory = {};

        factory.insertUserApplication = function (userApplication) {
            return $http.post(serviceBase + "/addUserApplication", userApplication).then(function (results) {
                UserApplication.id = results.data.id;
                return results.data;
            });
        }

        factory.updateUserApplication = function (userApplication) {
            return $http.post(serviceBase + "/updateUserApplication", userApplication).then(function (results) {
                UserApplication.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteUserApplication = function (id) {
            return $http.delete(serviceBase + '/deleteUserApplication/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getUserApplication = function (id) {
            return $http.get(serviceBase + '/getUserApplicationById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchUserApplication = function (userApplicationCriteria) {
            return $http.post(serviceBase + "/searchUserApplication", userApplicationCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    UserApplicationsFactory.$inject = injectParams;
    app.factory('userApplicationsService', UserApplicationsFactory)
});
