'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var StudentsFactory = function ($http, $q) {
        var serviceBase = '/api/student';
        var factory = {};

        factory.insertStudent = function (student) {
            return $http.post(serviceBase + "/addStudent", student).then(function (results) {
                Student.id = results.data.id;
                return results.data;
            });
        }

        factory.updateStudent = function (student) {
            return $http.post(serviceBase + "/updateStudent", student).then(function (results) {
                Student.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteStudent = function (id) {
            return $http.delete(serviceBase + '/deleteStudent/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getStudent = function (id) {
            return $http.get(serviceBase + '/getStudentById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchStudent = function (studentCriteria) {
            return $http.post(serviceBase + "/searchStudent", studentCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    StudentsFactory.$inject = injectParams;
    app.factory('studentsService', StudentsFactory)
});
