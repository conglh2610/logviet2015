require.config({
    baseUrl: '/app',
    urlArgs: 'v=1.0'
});


require(
    [
        'app'
        , 'assets/js/common'
        , 'shared/animations/listAnimations'
        , 'shared/directives/sampleDirective'
        , 'shared/services/routeResolver'
        , 'shared/services/authService'
        , 'shared/services/modalService'
        , 'shared/navigator/navbarController'
        , 'services/cars/carCategoriesService'
        , 'services/cars/carsService'
        , 'services/cars/carModelsService'
        , 'services/cars/carItemsService'
        , 'services/cars/driversService'

    ],

    function () {
        angular.bootstrap(document, ['logvietApp']);
    });

