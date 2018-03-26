app.config(function ($locationProvider, $routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'app/main/views/index.html'
        })
        .otherwise({ redirectTo: '/' });
});