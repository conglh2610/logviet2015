'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CountriesFactory = function ($http, $q) {
        var serviceBase = '/api/country';
        var factory = {};

        factory.insertCountry = function (country) {
            return $http.post(serviceBase + "/addCountry", country).then(function (results) {
                Country.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCountry = function (country) {
            return $http.post(serviceBase + "/updateCountry", country).then(function (results) {
                Country.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCountry = function (id) {
            return $http.delete(serviceBase + '/deleteCountry/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCountry = function (id) {
            return $http.get(serviceBase + '/getCountryById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCountry = function (countryCriteria) {
            return $http.post(serviceBase + "/searchCountry", countryCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CountriesFactory.$inject = injectParams;
    app.factory('countriesService', CountriesFactory)
});
