'use strict';

define(['app'], function (app) {


    var injectParams = ['$scope', '$location', 'carItemsService', 'carModelsService', 'carCategoriesService', 'modalService'];

    var carItemsController = function ($scope, $location, carItemsService, carModelsService, carCategoriesService, modalService) {

        var vm = this;

        vm.carItems = {};
        vm.carCategories = {};
        vm.carModels = {};
        vm.carItemsColumns = null;
        vm.title = "Quản lý đầu xe";

        vm.maxSize = 6;
        vm.totalItems = 0;
        vm.currentPage = 1;
        vm.itemPerPage = 4;
        vm.sortColumn = 'Name';
        vm.sortDirection = true;

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getCarItems();
        };

        vm.setOrder = function (columnName) {
            if (columnName === vm.sortColumn) {
                vm.sortDirection = !vm.sortDirection;
            }
            vm.sortColumn = columnName;

            vm.currentPage = 1;
            getCarItems();
        }

        function getCarItems() {
            var criteria = {
                currentPage: vm.currentPage - 1,
                itemPerPage: vm.itemPerPage,
                sortColumn: vm.sortColumn,
                sortDirection: vm.sortDirection
            };

            carItemsService.getCarItemsByCriteria(criteria).then(function (results) {
                debugger;
                vm.carItems = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);

        };

        function getCarCategories() {
            carCategoriesService.getCarCategories().then(function (data) {
                vm.carCategories = data;
            }, $scope.processError);
        }

        function getCarModels() {
            carModelsService.getCarModels().then(function (data) {
                vm.carModels = data;
            }, $scope.processError)
        }

        vm.initialData = function () {

            vm.currentPage = 1;

            getCarItems();
            getCarCategories();
            getCarModels();

            // create column
            if (!vm.carItemsColumns) {
                vm.carItemsColumns = [
                  { columnName: 'categoryid', displayName: 'Danh mục xe', allowSort: true, sortDirection: 'asc', span: 1 },
                   { columnName: 'modelid', displayName: 'Đời xe', allowSort: true, sortDirection: 'asc', span: 1 },
                   { columnName: 'carid', displayName: 'Tên xe', allowSort: true, sortDirection: 'asc', span: 1 }];
            }
        }

        vm.initialData();

        vm.navigate = function (url) {
            $location.path(url);
        };

        vm.deleteCarItem = function (carItem) {
            carItemsService.deleteCarItem(carItem.Id).then(function (response) {
                getCarItems();
            }, $scope.processError);
        }

        vm.search = function () {

            var criteria = {
                currentPage: vm.currentPage - 1,
                itemPerPage: vm.itemPerPage,
                sortColumn: vm.sortColumn,
                sortDirection: vm.sortDirection,
                carName: vm.search.searchText,
                carCategoryId: vm.search.carCategoryId,
                carModelId: vm.search.carModelId
            };

            carItemsService.getCarItemsByCriteria(criteria).then(function (results) {
                vm.carItems = results.data;
                vm.totalItems = parseInt(results.headers.inlinecount);
            }, $scope.processError);
        }
    }

    carItemsController.$inject = injectParams;

    app.register.controller('carItemsController', carItemsController);
});