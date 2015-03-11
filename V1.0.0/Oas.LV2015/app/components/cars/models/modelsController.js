'use strict';

define(['app'], function (app) {


    var injectParams = ['$scope', '$location', 'carModelsService','carCategoriesService', 'modalService'];

    var modelsController = function ($scope, $location, carModelsService,carCategoriesService, modalService) {

        var vm = this;

        vm.carModels = {};
        vm.carModelsColumns = null;
        vm.title = "Quản lý đời xe";

        vm.maxSize = 6;
        vm.totalItems = 0;
        vm.currentPage = 1;
        vm.itemPerPage = 4;
        vm.sortColumn = 'Name';
        vm.sortDirection = true;

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            vm.getCarModels();
        };

        vm.setOrder = function (columnName) {
            if (columnName === vm.sortColumn) {
                vm.sortDirection = !vm.sortDirection;
            }
            vm.sortColumn = columnName;

            vm.currentPage = 1;
            vm.getCarModels();
        }

        vm.getCarModels = function () {
            var criteria = {
                currentPage: vm.currentPage - 1,
                itemPerPage: vm.itemPerPage,
                sortColumn: vm.sortColumn,
                sortDirection: vm.sortDirection
            };

            carModelsService.getCarModelsByCriteria(criteria).then(function (results) {
                debugger;
                vm.carModels = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);

        };

        vm.initialData = function () {

            vm.currentPage = 1;

            vm.getCarModels();

            // create column
            if (!vm.carModelsColumns) {
                vm.carModelsColumns = [
                  { columnName: 'CategoryId', displayName: 'Danh mục xe', allowSort: true, sortDirection: 'asc', span: 1 },
                   { columnName: 'Name', displayName: 'Đời xe', allowSort: true, sortDirection: 'asc', span: 1 }];
            }
        }

        vm.initialData();

        vm.navigate = function (url) {
            $location.path(url);
        };

        vm.deleteCarModel = function (carModel) {
            carModelsService.deleteCarModel(carModel.Id).then(function (response) {
                vm.getCarModels();
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

            carModelsService.getCarModelsByCriteria(criteria).then(function (results) {
                vm.carModels = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);
        }
    }

    modelsController.$inject = injectParams;

    app.register.controller('modelsController', modelsController);
});