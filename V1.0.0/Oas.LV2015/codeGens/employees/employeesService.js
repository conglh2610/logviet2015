'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var EmployeesFactory = function ($http, $q) {
        var serviceBase = '/api/employee';
        var factory = {};

        factory.insertEmployee = function (employee) {
            return $http.post(serviceBase + "/addEmployee", employee).then(function (results) {
                Employee.id = results.data.id;
                return results.data;
            });
        }

        factory.updateEmployee = function (employee) {
            return $http.post(serviceBase + "/updateEmployee", employee).then(function (results) {
                Employee.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteEmployee = function (id) {
            return $http.delete(serviceBase + '/deleteEmployee/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getEmployee = function (id) {
            return $http.get(serviceBase + '/getEmployeeById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchEmployee = function (employeeCriteria) {
            return $http.post(serviceBase + "/searchEmployee", employeeCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    EmployeesFactory.$inject = injectParams;
    app.factory('employeesService', EmployeesFactory)
});
