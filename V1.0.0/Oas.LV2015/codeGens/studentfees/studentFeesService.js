'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var StudentFeesFactory = function ($http, $q) {
        var serviceBase = '/api/studentFee';
        var factory = {};

        factory.insertStudentFee = function (studentFee) {
            return $http.post(serviceBase + "/addStudentFee", studentFee).then(function (results) {
                StudentFee.id = results.data.id;
                return results.data;
            });
        }

        factory.updateStudentFee = function (studentFee) {
            return $http.post(serviceBase + "/updateStudentFee", studentFee).then(function (results) {
                StudentFee.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteStudentFee = function (id) {
            return $http.delete(serviceBase + '/deleteStudentFee/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getStudentFee = function (id) {
            return $http.get(serviceBase + '/getStudentFeeById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchStudentFee = function (studentFeeCriteria) {
            return $http.post(serviceBase + "/searchStudentFee", studentFeeCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    StudentFeesFactory.$inject = injectParams;
    app.factory('studentFeesService', StudentFeesFactory)
});
