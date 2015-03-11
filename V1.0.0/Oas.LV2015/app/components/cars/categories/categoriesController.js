
'use strict';

define(['app'], function (app) {


    var injectParams = ['$scope', '$location', 'carCategoriesService', 'modalService'];

    var CategoriesController = function ($scope, $location, carCategoriesService, modalService) {

        var vm = this;

        vm.carCategories = {};
        vm.carCategoriesColumns = null;
        vm.title = "Quản lý danh mục xe";

        vm.maxSize = 6;
        vm.totalItems = 0;
        vm.currentPage = 1;
        vm.itemPerPage = 4;
        vm.sortColumn = 'name';
        vm.sortDirection = 'asc';

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            vm.getCarCategories();
        };

        vm.setOrder = function (columnName) {
            if (columnName === vm.sortColumn) {
                vm.sortDirection = !vm.sortDirection;
            }
            vm.sortColumn = columnName;

            vm.currentPage = 1;
            vm.getCarCategories();
        }

        vm.getCarCategories = function () {
            var criteria = {
                currentPage: vm.currentPage - 1,
                itemPerPage: vm.itemPerPage,
                sortColumn: vm.sortColumn,
                sortDirection: vm.sortDirection
            };

            carCategoriesService.getCarCategoriesByCriteria(criteria).then(function (results) {
                vm.carCategories = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);
        };

        vm.initialData = function () {

            vm.currentPage = 1;

            vm.getCarCategories();

            // create column
            if (!vm.carCategoriesColumns) {
                vm.carCategoriesColumns = [
                  { columnName: 'name', displayName: 'Tên danh mục xe', allowSort: true, sortDirection: 'asc', span: 1 }]
            }
        }

        vm.initialData();

        vm.navigate = function (url) {
            $location.path(url);
        };

        vm.deleteCarCategory = function (carCategory) {
            debugger;
            carCategoriesService.deleteCarCategory(carCategory.Id).then(function (response) {
                vm.getCarCategories();
            }, $scope.processError);
        }

        vm.search = function () {
            debugger;
            var criteria = {
                currentPage: vm.currentPage - 1,
                itemPerPage: vm.itemPerPage,
                sortColumn: vm.sortColumn,
                sortDirection: vm.sortDirection,
                name : vm.search.searchText
            };

            carCategoriesService.getCarCategoriesByCriteria(criteria).then(function (results) {
                vm.carCategories = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);
        }
    }

    CategoriesController.$inject = injectParams;

    app.register.controller('CategoriesController', CategoriesController);
});