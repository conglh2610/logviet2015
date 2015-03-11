'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var PackageItemsFactory = function ($http, $q) {
        var serviceBase = '/api/packageItem';
        var factory = {};

        factory.insertPackageItem = function (packageItem) {
            return $http.post(serviceBase + "/addPackageItem", packageItem).then(function (results) {
                PackageItem.id = results.data.id;
                return results.data;
            });
        }

        factory.updatePackageItem = function (packageItem) {
            return $http.post(serviceBase + "/updatePackageItem", packageItem).then(function (results) {
                PackageItem.id = results.data.id;
                return results.data;
            });
        }


        factory.deletePackageItem = function (id) {
            return $http.delete(serviceBase + '/deletePackageItem/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getPackageItem = function (id) {
            return $http.get(serviceBase + '/getPackageItemById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchPackageItem = function (packageItemCriteria) {
            return $http.post(serviceBase + "/searchPackageItem", packageItemCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    PackageItemsFactory.$inject = injectParams;
    app.factory('packageItemsService', PackageItemsFactory)
});
