'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ClassTeachersFactory = function ($http, $q) {
        var serviceBase = '/api/classTeacher';
        var factory = {};

        factory.insertClassTeacher = function (classTeacher) {
            return $http.post(serviceBase + "/addClassTeacher", classTeacher).then(function (results) {
                ClassTeacher.id = results.data.id;
                return results.data;
            });
        }

        factory.updateClassTeacher = function (classTeacher) {
            return $http.post(serviceBase + "/updateClassTeacher", classTeacher).then(function (results) {
                ClassTeacher.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteClassTeacher = function (id) {
            return $http.delete(serviceBase + '/deleteClassTeacher/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getClassTeacher = function (id) {
            return $http.get(serviceBase + '/getClassTeacherById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchClassTeacher = function (classTeacherCriteria) {
            return $http.post(serviceBase + "/searchClassTeacher", classTeacherCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ClassTeachersFactory.$inject = injectParams;
    app.factory('classTeachersService', ClassTeachersFactory)
});
