'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var EmailTemplatesFactory = function ($http, $q) {
        var serviceBase = '/api/emailTemplate';
        var factory = {};

        factory.insertEmailTemplate = function (emailTemplate) {
            return $http.post(serviceBase + "/addEmailTemplate", emailTemplate).then(function (results) {
                EmailTemplate.id = results.data.id;
                return results.data;
            });
        }

        factory.updateEmailTemplate = function (emailTemplate) {
            return $http.post(serviceBase + "/updateEmailTemplate", emailTemplate).then(function (results) {
                EmailTemplate.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteEmailTemplate = function (id) {
            return $http.delete(serviceBase + '/deleteEmailTemplate/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getEmailTemplate = function (id) {
            return $http.get(serviceBase + '/getEmailTemplateById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchEmailTemplate = function (emailTemplateCriteria) {
            return $http.post(serviceBase + "/searchEmailTemplate", emailTemplateCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    EmailTemplatesFactory.$inject = injectParams;
    app.factory('emailTemplatesService', EmailTemplatesFactory)
});
