'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var studentsFactory = function ($http, $q) {
        var serviceBase = '/api/students';
        var factory = {};

        factory.insertStudent = function (student) {
            return $http.post(serviceBase + "/addStudent", student).then(function (results) {
                student.id = results.data.id;
                return results.data;
            });
        }

        factory.deleteStudent = function (id) {
            return $http.delete(serviceBase + '/deleteStudent/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getStudents = function (pageIndex, pageSize) {
            return getPagedResource(serviceBase + '/getStudents', pageIndex, pageSize);
        };

        factory.search = function (criteria) {
            return $http.post(serviceBase + "/search", criteria).then(function (response) {
                return response;
            });


        }

        factory.getStudent = function (id) {
            return $http.get(serviceBase + '/getStudentById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.updateStudent = function (student) {
            return $http.put(serviceBase + '/putStudent/' + student.Id, student).then(function (status) {
                return status.data;
            });
        };

        factory.getStudentClassHistories = function (studentId, criteria) {
            return $http.post(serviceBase + "/ViewStudentClassHistory?studentId=" + studentId, criteria).then(function (response) {
                return response;
            });
        }


        return factory;

    }

    studentsFactory.$inject = injectParams;
    app.factory('studentsService', studentsFactory)
});