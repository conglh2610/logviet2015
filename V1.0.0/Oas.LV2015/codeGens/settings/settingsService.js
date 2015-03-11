'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var SettingsFactory = function ($http, $q) {
        var serviceBase = '/api/setting';
        var factory = {};

        factory.insertSetting = function (setting) {
            return $http.post(serviceBase + "/addSetting", setting).then(function (results) {
                Setting.id = results.data.id;
                return results.data;
            });
        }

        factory.updateSetting = function (setting) {
            return $http.post(serviceBase + "/updateSetting", setting).then(function (results) {
                Setting.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteSetting = function (id) {
            return $http.delete(serviceBase + '/deleteSetting/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getSetting = function (id) {
            return $http.get(serviceBase + '/getSettingById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchSetting = function (settingCriteria) {
            return $http.post(serviceBase + "/searchSetting", settingCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    SettingsFactory.$inject = injectParams;
    app.factory('settingsService', SettingsFactory)
});
