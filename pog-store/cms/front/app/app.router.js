app.config(function ($locationProvider, $routeProvider) {
    $routeProvider
        .when('/users', {
            templateUrl: 'app/users/list.html',
            controller: 'UserController'
        })
        .otherwise({ redirectTo: '/' });
});