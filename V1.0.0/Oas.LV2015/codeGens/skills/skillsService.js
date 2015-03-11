'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var SkillsFactory = function ($http, $q) {
        var serviceBase = '/api/skill';
        var factory = {};

        factory.insertSkill = function (skill) {
            return $http.post(serviceBase + "/addSkill", skill).then(function (results) {
                Skill.id = results.data.id;
                return results.data;
            });
        }

        factory.updateSkill = function (skill) {
            return $http.post(serviceBase + "/updateSkill", skill).then(function (results) {
                Skill.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteSkill = function (id) {
            return $http.delete(serviceBase + '/deleteSkill/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getSkill = function (id) {
            return $http.get(serviceBase + '/getSkillById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchSkill = function (skillCriteria) {
            return $http.post(serviceBase + "/searchSkill", skillCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    SkillsFactory.$inject = injectParams;
    app.factory('skillsService', SkillsFactory)
});
