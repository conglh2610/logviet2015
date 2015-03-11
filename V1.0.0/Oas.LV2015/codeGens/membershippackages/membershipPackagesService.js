'use strict';

define(['app'], function (app) {
    var injectParams = ['$http', '$q'];

    var MembershipPackagesFactory = function ($http, $q) {
        var serviceBase = '/api/membershipPackage';
        var factory = {};

        factory.insertMembershipPackage = function (membershipPackage) {
            return $http.post(serviceBase + "/addMembershipPackage", membershipPackage).then(function (results) {
                MembershipPackage.id = results.data.id;
                return results.data;
            });
        }

        factory.updateMembershipPackage = function (membershipPackage) {
            return $http.post(serviceBase + "/updateMembershipPackage", membershipPackage).then(function (results) {
                MembershipPackage.id = results.data.id;
                return results.data;
            });
        }


        factory.deleteMembershipPackage = function (id) {
            return $http.delete(serviceBase + '/deleteMembershipPackage/' + id).then(function (status) {
                return status.data;
            });
        }

        factory.getMembershipPackage = function (id) {
            return $http.get(serviceBase + '/getMembershipPackageById/' + id).then(function (results) {
                return results.data;
            });
        }

        factory.searchMembershipPackage = function (membershipPackageCriteria) {
            return $http.post(serviceBase + "/searchMembershipPackage", membershipPackageCriteria).then(function (response) {
                return response;
            });
        };


        return factory;

    }

    MembershipPackagesFactory.$inject = injectParams;
    app.factory('membershipPackagesService', MembershipPackagesFactory)
});
