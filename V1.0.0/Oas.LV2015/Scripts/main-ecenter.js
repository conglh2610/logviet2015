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
        , 'shared/directives/compile'
        , 'shared/directives/checkUnique'
         ,'services/ecenters/roomsService'
         ,'services/ecenters/studentsService'
         ,'shared/filters/ecenters/roomsFilter'
    ],

    function () {
        angular.bootstrap(document, ['logvietApp']);
    });

