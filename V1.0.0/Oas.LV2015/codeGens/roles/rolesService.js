'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var RolesFactory = function ($http, $q) {
        var serviceBase = '/api/role';
        var factory = {};

        factory.insertRole = function (role) {
            return $http.post(serviceBase + "/addRole", role).then(function (results) {
                Role.id = results.data.id;
                return results.data;
            });
        }

        factory.updateRole = function (role) {
            return $http.post(serviceBase + "/updateRole", role).then(function (results) {
                Role.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteRole = function (id) {
            return $http.delete(serviceBase + '/deleteRole/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getRole = function (id) {
            return $http.get(serviceBase + '/getRoleById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchRole = function (roleCriteria) {
            return $http.post(serviceBase + "/searchRole", roleCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    RolesFactory.$inject = injectParams;
    app.factory('rolesService', RolesFactory)
});
