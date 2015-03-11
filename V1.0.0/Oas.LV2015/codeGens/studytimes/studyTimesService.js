'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var StudyTimesFactory = function ($http, $q) {
        var serviceBase = '/api/studyTime';
        var factory = {};

        factory.insertStudyTime = function (studyTime) {
            return $http.post(serviceBase + "/addStudyTime", studyTime).then(function (results) {
                StudyTime.id = results.data.id;
                return results.data;
            });
        }

        factory.updateStudyTime = function (studyTime) {
            return $http.post(serviceBase + "/updateStudyTime", studyTime).then(function (results) {
                StudyTime.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteStudyTime = function (id) {
            return $http.delete(serviceBase + '/deleteStudyTime/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getStudyTime = function (id) {
            return $http.get(serviceBase + '/getStudyTimeById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchStudyTime = function (studyTimeCriteria) {
            return $http.post(serviceBase + "/searchStudyTime", studyTimeCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    StudyTimesFactory.$inject = injectParams;
    app.factory('studyTimesService', StudyTimesFactory)
});
