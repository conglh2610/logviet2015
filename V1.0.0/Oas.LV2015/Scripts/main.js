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
        //, 'services/ecenters/studentsService',
        //, 'services/ecenters/roomsService',
        , 'services/cars/carCategoriesService'
        , 'services/cars/carModelsService'
        , 'services/cars/carsService'
        //, 'shared/directives/uploadFile'
        , 'shared/directives/checkUnique'

    ],

    function () {
        angular.bootstrap(document, ['logvietApp']);
    });

