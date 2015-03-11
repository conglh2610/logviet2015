'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var LanguagesFactory = function ($http, $q) {
        var serviceBase = '/api/language';
        var factory = {};

        factory.insertLanguage = function (language) {
            return $http.post(serviceBase + "/addLanguage", language).then(function (results) {
                Language.id = results.data.id;
                return results.data;
            });
        }

        factory.updateLanguage = function (language) {
            return $http.post(serviceBase + "/updateLanguage", language).then(function (results) {
                Language.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteLanguage = function (id) {
            return $http.delete(serviceBase + '/deleteLanguage/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getLanguage = function (id) {
            return $http.get(serviceBase + '/getLanguageById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchLanguage = function (languageCriteria) {
            return $http.post(serviceBase + "/searchLanguage", languageCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    LanguagesFactory.$inject = injectParams;
    app.factory('languagesService', LanguagesFactory)
});
