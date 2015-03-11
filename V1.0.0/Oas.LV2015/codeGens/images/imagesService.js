'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var ImagesFactory = function ($http, $q) {
        var serviceBase = '/api/image';
        var factory = {};

        factory.insertImage = function (image) {
            return $http.post(serviceBase + "/addImage", image).then(function (results) {
                Image.id = results.data.id;
                return results.data;
            });
        }

        factory.updateImage = function (image) {
            return $http.post(serviceBase + "/updateImage", image).then(function (results) {
                Image.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteImage = function (id) {
            return $http.delete(serviceBase + '/deleteImage/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getImage = function (id) {
            return $http.get(serviceBase + '/getImageById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchImage = function (imageCriteria) {
            return $http.post(serviceBase + "/searchImage", imageCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    ImagesFactory.$inject = injectParams;
    app.factory('imagesService', ImagesFactory)
});
