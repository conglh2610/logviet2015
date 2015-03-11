'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$filter','authService', '$location', '$window',
                        '$timeout', 'imagesService', 'modalService'];

    var ImagesController = function ($scope, $filter,authService, $location, $window,
        $timeout, imagesService, modalService) {

        var vm = this;
        vm.images = [];
        vm.filteredImages = [];
        vm.filteredCount = 0;
        vm.currentPage = 1;
        vm.itemPerPage = 10;
        vm.totalRecords = 0;
        vm.errorMessage = "";
        vm.sortDirection = "asc";
        vm.columnName = "name";
        vm.itemPerPage = 5;

        vm.searchText = "";
        vm.cardAnimationClass = '.card-animation';
        vm.DisplayModeEnum = {            
            List: 0,
            Card: 1
        };


        vm.changeDisplayMode = function (displayMode) {

            switch (displayMode) {
                case vm.DisplayModeEnum.Card:
                    vm.listDisplayModeEnabled = false;
                    break;
                case vm.DisplayModeEnum.List:
                    vm.listDisplayModeEnabled = true;
                    break;
            }
        };

        function processError(error) {
            vm.errorMessage = error.message;
            alert(error);
        };

        /*region ==> Get Data*/

        vm.searchTextChanged = function () {
            filterimages(vm.searchText);
        };


        function initalData() {
            vm.currentPage = 1;
            getImages();
        }


        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getimages();
        }


        function filterImages(filterText) {
            
            vm.filteredImages = $filter("imageFilter")(vm.images, filterText);
            vm.filteredCount = vm.filteredImages.length;
        }

		vm.deleteImage = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var image = getImageById(id);
            var imageName = Image;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Image',
                headerText: 'Delete ' + imageName + '?',
                bodyText: 'Are you sure you want to delete this '+imageName+' ?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    imagesService.deleteImage(id).then(function () {
                        for (var i = 0; i < vm.images.length; i++) {
                            if (vm.images[i].id === id) {
                                vm.images.splice(i, 1);
                                break;
                            }
							if (vm.filteredImages[i].id === id) {
                                vm.filteredImages.splice(i, 1);
                                break;
                            }
                        }
                        filterImages(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting image: ' + error.message);
                    });
                }
            });
        };

		function getImagesById(id) {
            for (var i = 0; i < vm.images.length; i++) {
                var image = vm.images[i];
                if (image.id === id) {
                    return image;
                }
            }
            return null;
        }

        function getImages() {

            var criteria = {
                CurrentPage: vm.currentPage-1,
                ItemPerPage: vm.itemPerPage,
                SortColumn: vm.columnName,
                SortDirection: vm.sortDirection
            };

            imagesService.searchImage(criteria).then(function (response) {
                vm.totalRecords = parseInt(response.headers('X-InlineCount'));
                vm.images = response.data;
                filterImages(vm.searchText);
            });

            
        }
        /*END*/


        vm.changeDisplayMode = function (displayMode) {
            switch (displayMode) {
                case vm.DisplayModeEnum.Card:
                    vm.listDisplayModeEnabled = false;
                    break;
                case vm.DisplayModeEnum.List:
                    vm.listDisplayModeEnabled = true;
                    break;
            }
        };

        initalData();

    };

    ImagesController.$inject = injectParams;

    app.register.controller('ImagesController', ImagesController);

});
