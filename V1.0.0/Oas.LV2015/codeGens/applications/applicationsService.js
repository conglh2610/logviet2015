'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ApplicationsFactory = function ($http, $q) {
        var serviceBase = '/api/application';
        var factory = {};

        factory.insertApplication = function (application) {
            return $http.post(serviceBase + "/addApplication", application).then(function (results) {
                Application.id = results.data.id;
                return results.data;
            });
        }

        factory.updateApplication = function (application) {
            return $http.post(serviceBase + "/updateApplication", application).then(function (results) {
                Application.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteApplication = function (id) {
            return $http.delete(serviceBase + '/deleteApplication/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getApplication = function (id) {
            return $http.get(serviceBase + '/getApplicationById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchApplication = function (applicationCriteria) {
            return $http.post(serviceBase + "/searchApplication", applicationCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ApplicationsFactory.$inject = injectParams;
    app.factory('applicationsService', ApplicationsFactory)
});
