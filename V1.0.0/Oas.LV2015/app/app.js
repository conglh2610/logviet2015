
define(['app'], function () {
    var app = angular.module('logvietApp', ['ngRoute', 'ngAnimate', 'routeResolverServices',
                                          'wc.directives', 'wc.animations', 'ui.bootstrap', 'google-maps']);

    app.config(['$routeProvider', 'routeResolverProvider', '$controllerProvider',
                    '$compileProvider', '$filterProvider', '$provide', '$httpProvider',

            function ($routeProvider, routeResolverProvider, $controllerProvider,
                      $compileProvider, $filterProvider, $provide, $httpProvider) {


                app.register =
                {
                    controller: $controllerProvider.register,
                    directive: $compileProvider.directive,
                    filter: $filterProvider.register,
                    factory: $provide.factory,
                    service: $provide.service
                };

                //Define routes - controllers will be loaded dynamically
                var route = routeResolverProvider.route;

                $routeProvider
                    .when('/home', route.resolve('Home', 'home/', 'vm'))
                    .when('/ecenters', route.resolve('Ecenters', 'ecenters/', 'vm'))
                    .when('/login/:redirect*?', route.resolve('login', 'login/', 'vm'))
                    //-----------------> routing for car renting management >>-----------------
                    .when('/categories', route.resolve('Categories', 'cars/categories/', 'vm'))
                    .when('/carCategoryItem/:carCategoryId', route.resolve('categoryItem', 'cars/categories/', 'vm', true))
                    .when('/models', route.resolve('models', 'cars/models/', 'vm'))
                    .when('/carModelItem/:carModelId', route.resolve('modelItem', 'cars/models/', 'vm', true))
                    .when('/cars', route.resolve('cars', 'cars/cars/', 'vm'))
                    .when('/carItem/:carId', route.resolve('carItem', 'cars/cars/', 'vm', true))
                    .when('/carItems', route.resolve('carItems', 'cars/carItems/', 'vm'))
                    .when('/carItemItem/:carItemId', route.resolve('carItemItem', 'cars/carItems/', 'vm', true))
                    .when('/drivers', route.resolve('drivers', 'cars/drivers/', 'vm'))
                    .when('/driveritem/:driverId', route.resolve('driverItem', 'cars/drivers/', 'vm', true))
                    //-----------------> routing for car ecenter management >>-----------------
                    .when('/students', route.resolve('Students', 'ecenters/students/', 'vm'))
                    .when('/studentitem/:studentId', route.resolve('StudentItem', 'ecenters/students/', 'vm', true))
                    .when('/studentClassHistory/:studentId', route.resolve('StudentClassHistory', 'ecenters/students/', 'vm'))
                    .when('/rooms', route.resolve('Rooms', 'ecenters/rooms/', 'vm'))
                    .when('/roomItem/:roomId', route.resolve('RoomItem', 'ecenters/rooms/', 'vm'))
                    //end ecenter app
                    .otherwise({ redirectTo: '/home' });

            }]);

    app.run(['$rootScope', '$location', 'authService',
        function ($rootScope, $location, authService) {

            //Client-side security. Server-side framework MUST add it's 
            //own security as well since client-based security is easily hacked
            $rootScope.$on("$routeChangeStart", function (event, next, current) {
                if (next && next.$$route && next.$$route.secure) {
                    if (!authService.user.isAuthenticated) {
                        $rootScope.$evalAsync(function () {
                            authService.redirectToLogin();
                        });
                    }
                }
            });

            $rootScope.loadPage = function (href) {
                var xmlhttp = new XMLHttpRequest();
                xmlhttp.open("GET", href, false);
                xmlhttp.send();
                return xmlhttp.responseText;
            };

            $rootScope.processError = function (error) {
                alert(error.Message);
            }

        }]);

    return app;

});
