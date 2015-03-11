'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var RoomsFactory = function ($http, $q) {
        var serviceBase = '/api/room';
        var factory = {};

        factory.insertRoom = function (room) {
            return $http.post(serviceBase + "/addRoom", room).then(function (results) {
                Room.id = results.data.id;
                return results.data;
            });
        }

        factory.updateRoom = function (room) {
            return $http.post(serviceBase + "/updateRoom", room).then(function (results) {
                Room.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteRoom = function (id) {
            return $http.delete(serviceBase + '/deleteRoom/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getRoom = function (id) {
            return $http.get(serviceBase + '/getRoomById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchRoom = function (roomCriteria) {
            return $http.post(serviceBase + "/searchRoom", roomCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    RoomsFactory.$inject = injectParams;
    app.factory('roomsService', RoomsFactory)
});
