'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var UsersFactory = function ($http, $q) {
        var serviceBase = '/api/user';
        var factory = {};

        factory.insertUser = function (user) {
            return $http.post(serviceBase + "/addUser", user).then(function (results) {
                User.id = results.data.id;
                return results.data;
            });
        }

        factory.updateUser = function (user) {
            return $http.post(serviceBase + "/updateUser", user).then(function (results) {
                User.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteUser = function (id) {
            return $http.delete(serviceBase + '/deleteUser/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getUser = function (id) {
            return $http.get(serviceBase + '/getUserById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchUser = function (userCriteria) {
            return $http.post(serviceBase + "/searchUser", userCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    UsersFactory.$inject = injectParams;
    app.factory('usersService', UsersFactory)
});
