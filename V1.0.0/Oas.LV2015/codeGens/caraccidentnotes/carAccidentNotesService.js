'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var CarAccidentNotesFactory = function ($http, $q) {
        var serviceBase = '/api/carAccidentNote';
        var factory = {};

        factory.insertCarAccidentNote = function (carAccidentNote) {
            return $http.post(serviceBase + "/addCarAccidentNote", carAccidentNote).then(function (results) {
                CarAccidentNote.id = results.data.id;
                return results.data;
            });
        }

        factory.updateCarAccidentNote = function (carAccidentNote) {
            return $http.post(serviceBase + "/updateCarAccidentNote", carAccidentNote).then(function (results) {
                CarAccidentNote.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteCarAccidentNote = function (id) {
            return $http.delete(serviceBase + '/deleteCarAccidentNote/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getCarAccidentNote = function (id) {
            return $http.get(serviceBase + '/getCarAccidentNoteById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchCarAccidentNote = function (carAccidentNoteCriteria) {
            return $http.post(serviceBase + "/searchCarAccidentNote", carAccidentNoteCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    CarAccidentNotesFactory.$inject = injectParams;
    app.factory('carAccidentNotesService', CarAccidentNotesFactory)
});
