'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var BookingNotesFactory = function ($http, $q) {
        var serviceBase = '/api/bookingNote';
        var factory = {};

        factory.insertBookingNote = function (bookingNote) {
            return $http.post(serviceBase + "/addBookingNote", bookingNote).then(function (results) {
                BookingNote.id = results.data.id;
                return results.data;
            });
        }

        factory.updateBookingNote = function (bookingNote) {
            return $http.post(serviceBase + "/updateBookingNote", bookingNote).then(function (results) {
                BookingNote.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteBookingNote = function (id) {
            return $http.delete(serviceBase + '/deleteBookingNote/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getBookingNote = function (id) {
            return $http.get(serviceBase + '/getBookingNoteById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchBookingNote = function (bookingNoteCriteria) {
            return $http.post(serviceBase + "/searchBookingNote", bookingNoteCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    BookingNotesFactory.$inject = injectParams;
    app.factory('bookingNotesService', BookingNotesFactory)
});
