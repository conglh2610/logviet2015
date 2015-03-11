'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var UserRolesFactory = function ($http, $q) {
        var serviceBase = '/api/userRole';
        var factory = {};

        factory.insertUserRole = function (userRole) {
            return $http.post(serviceBase + "/addUserRole", userRole).then(function (results) {
                UserRole.id = results.data.id;
                return results.data;
            });
        }

        factory.updateUserRole = function (userRole) {
            return $http.post(serviceBase + "/updateUserRole", userRole).then(function (results) {
                UserRole.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteUserRole = function (id) {
            return $http.delete(serviceBase + '/deleteUserRole/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getUserRole = function (id) {
            return $http.get(serviceBase + '/getUserRoleById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchUserRole = function (userRoleCriteria) {
            return $http.post(serviceBase + "/searchUserRole", userRoleCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    UserRolesFactory.$inject = injectParams;
    app.factory('userRolesService', UserRolesFactory)
});
