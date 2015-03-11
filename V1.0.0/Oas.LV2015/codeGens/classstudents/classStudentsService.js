'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ClassStudentsFactory = function ($http, $q) {
        var serviceBase = '/api/classStudent';
        var factory = {};

        factory.insertClassStudent = function (classStudent) {
            return $http.post(serviceBase + "/addClassStudent", classStudent).then(function (results) {
                ClassStudent.id = results.data.id;
                return results.data;
            });
        }

        factory.updateClassStudent = function (classStudent) {
            return $http.post(serviceBase + "/updateClassStudent", classStudent).then(function (results) {
                ClassStudent.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteClassStudent = function (id) {
            return $http.delete(serviceBase + '/deleteClassStudent/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getClassStudent = function (id) {
            return $http.get(serviceBase + '/getClassStudentById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchClassStudent = function (classStudentCriteria) {
            return $http.post(serviceBase + "/searchClassStudent", classStudentCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ClassStudentsFactory.$inject = injectParams;
    app.factory('classStudentsService', ClassStudentsFactory)
});
