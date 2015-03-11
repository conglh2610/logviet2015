'use strict';

define(['app'], function (app) {


    var injectParams = ['$scope', '$location', 'carsService', 'carModelsService', 'modalService'];

    var carsController = function ($scope, $location, carsService, carModelsService, modalService) {

        var vm = this;

        vm.cars = {};
        vm.carsColumns = null;
        vm.title = "Quản đời xe";

        vm.maxSize = 6;
        vm.totalItems = 0;
        vm.currentPage = 1;
        vm.itemPerPage = 4;
        vm.sortColumn = 'Name';
        vm.sortDirection = true;

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            vm.getCars();
        };

        vm.setOrder = function (columnName) {
            if (columnName === vm.sortColumn) {
                vm.sortDirection = !vm.sortDirection;
            }
            vm.sortColumn = columnName;

            vm.currentPage = 1;
            vm.getCars();
        }

        vm.getCars = function () {
            var criteria = {
                currentPage: vm.currentPage - 1,
                itemPerPage: vm.itemPerPage,
                sortColumn: vm.sortColumn,
                sortDirection: vm.sortDirection
            };

            carsService.getCarsByCriteria(criteria).then(function (results) {
                vm.cars = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);

        };

        vm.initialData = function () {

            vm.currentPage = 1;

            vm.getCars();

            // create column
            if (!vm.carsColumns) {
                vm.carsColumns = [
                  { columnName: 'carmodelid', displayName: 'Đời xe', allowSort: true, sortDirection: 'asc', span: 1 },
                   { columnName: 'Name', displayName: 'Tên xe', allowSort: true, sortDirection: 'asc', span: 1 }];
            }
        }

        vm.initialData();

        vm.navigate = function (url) {
            $location.path(url);
        };

        vm.deleteCar = function (car) {
            carsService.deleteCar(car.Id).then(function (response) {
                vm.getCars();
            }, $scope.processError);
        }

        vm.search = function () {

            var criteria = {
                currentPage: vm.currentPage - 1,
                itemPerPage: vm.itemPerPage,
                sortColumn: vm.sortColumn,
                sortDirection: vm.sortDirection,
                name: vm.search.searchText
            };

            carsService.getCarsByCriteria(criteria).then(function (results) {
                vm.cars = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);
        }
    }

    carsController.$inject = injectParams;

    app.register.controller('carsController', carsController);
});