'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ParentsFactory = function ($http, $q) {
        var serviceBase = '/api/parent';
        var factory = {};

        factory.insertParent = function (parent) {
            return $http.post(serviceBase + "/addParent", parent).then(function (results) {
                Parent.id = results.data.id;
                return results.data;
            });
        }

        factory.updateParent = function (parent) {
            return $http.post(serviceBase + "/updateParent", parent).then(function (results) {
                Parent.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteParent = function (id) {
            return $http.delete(serviceBase + '/deleteParent/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getParent = function (id) {
            return $http.get(serviceBase + '/getParentById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchParent = function (parentCriteria) {
            return $http.post(serviceBase + "/searchParent", parentCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ParentsFactory.$inject = injectParams;
    app.factory('parentsService', ParentsFactory)
});
