'use strict';

define(['app'], function (app) {

    var injectParams = ['$scope', '$location', '$routeParams',
                        '$timeout', 'config', 'countriesService', 'modalService'];

    var CountryItemController = function ($scope, $location, $routeParams,
                                           $timeout, config, countriesService, modalService) {

        var vm = this,
            countryId = ($routeParams.countryId) ? parseInt($routeParams.countryId) : 0,
            timer,
            onRouteChangeOff;

        vm.country = {};
        vm.title = (countryId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.buttonText = (countryId == '0') ? 'Cập nhật' : 'Thêm mới';
        vm.updateStatus = false;
        vm.errorMessage = '';


        vm.saveCountry = function () {
            if ($scope.itemForm.$valid) {
                if (!vm.country.id) {
                    countryService.insertCountry(vm.country).then(processSuccess, processError);
                }
                else {
                    countryService.updateCountry(vm.country).then(processSuccess, processError);
                }
            }
        };

        vm.deleteCountry = function () {
            var headerText = country;
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Country',
                headerText: 'Delete ' + headerText + '?',
                bodyText: 'Are you sure you want to delete this country?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    countryService.deleteCountry(vm.country.id).then(function () {
                        onRouteChangeOff(); //Stop listening for location changes
                        $location.path('/countrys');
                    }, processError);
                }
            });
        };

		
        function init() {
			
            //Make sure they're warned if they made a change but didn't save it
            //Call to $on returns a "deregistration" function that can be called to
            //remove the listener (see routeChange() for an example of using it)
            onRouteChangeOff = $scope.$on('$locationChangeStart', routeChange);
        }

        init();

        function routeChange(event, newUrl, oldUrl) {
            //Navigate to newUrl if the form isn't dirty
            if (!vm.itemForm || !vm.itemForm.$dirty) return;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Ignore Changes',
                headerText: 'Unsaved Changes',
                bodyText: 'You have unsaved changes. Leave the page?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    onRouteChangeOff(); //Stop listening for location changes
                    $location.path($location.url(newUrl).hash()); //Go to page they're interested in
                }
            });

            //prevent navigation by default since we'll handle it
            //once the user selects a dialog option
            event.preventDefault();
            return;
        }


        function processSuccess() {
            $scope.itemForm.$dirty = false;
            vm.updateStatus = true;
            vm.title = 'Edit';
            vm.buttonText = 'Update';
            startTimer();
        }

        function processError(error) {
            vm.errorMessage = error.message;
            startTimer();
        }

        function startTimer() {
            timer = $timeout(function () {
                $timeout.cancel(timer);
                vm.errorMessage = '';
                vm.updateStatus = false;
            }, 3000);
        }
    };

    CountryItemController.$inject = injectParams;

    app.register.controller('CountryItemController', CountryItemController);

});
