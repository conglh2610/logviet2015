'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ProgramsFactory = function ($http, $q) {
        var serviceBase = '/api/program';
        var factory = {};

        factory.insertProgram = function (program) {
            return $http.post(serviceBase + "/addProgram", program).then(function (results) {
                Program.id = results.data.id;
                return results.data;
            });
        }

        factory.updateProgram = function (program) {
            return $http.post(serviceBase + "/updateProgram", program).then(function (results) {
                Program.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteProgram = function (id) {
            return $http.delete(serviceBase + '/deleteProgram/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getProgram = function (id) {
            return $http.get(serviceBase + '/getProgramById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchProgram = function (programCriteria) {
            return $http.post(serviceBase + "/searchProgram", programCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ProgramsFactory.$inject = injectParams;
    app.factory('programsService', ProgramsFactory)
});
