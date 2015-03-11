'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var TeachersFactory = function ($http, $q) {
        var serviceBase = '/api/teacher';
        var factory = {};

        factory.insertTeacher = function (teacher) {
            return $http.post(serviceBase + "/addTeacher", teacher).then(function (results) {
                Teacher.id = results.data.id;
                return results.data;
            });
        }

        factory.updateTeacher = function (teacher) {
            return $http.post(serviceBase + "/updateTeacher", teacher).then(function (results) {
                Teacher.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteTeacher = function (id) {
            return $http.delete(serviceBase + '/deleteTeacher/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getTeacher = function (id) {
            return $http.get(serviceBase + '/getTeacherById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchTeacher = function (teacherCriteria) {
            return $http.post(serviceBase + "/searchTeacher", teacherCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    TeachersFactory.$inject = injectParams;
    app.factory('teachersService', TeachersFactory)
});
