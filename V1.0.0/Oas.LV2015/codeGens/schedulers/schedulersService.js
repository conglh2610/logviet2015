'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var SchedulersFactory = function ($http, $q) {
        var serviceBase = '/api/scheduler';
        var factory = {};

        factory.insertScheduler = function (scheduler) {
            return $http.post(serviceBase + "/addScheduler", scheduler).then(function (results) {
                Scheduler.id = results.data.id;
                return results.data;
            });
        }

        factory.updateScheduler = function (scheduler) {
            return $http.post(serviceBase + "/updateScheduler", scheduler).then(function (results) {
                Scheduler.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteScheduler = function (id) {
            return $http.delete(serviceBase + '/deleteScheduler/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getScheduler = function (id) {
            return $http.get(serviceBase + '/getSchedulerById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchScheduler = function (schedulerCriteria) {
            return $http.post(serviceBase + "/searchScheduler", schedulerCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    SchedulersFactory.$inject = injectParams;
    app.factory('schedulersService', SchedulersFactory)
});
