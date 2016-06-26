var app = angular.module('fb.lists', [
    'ui.bootstrap',
    'ui.bootstrap.popover',
    'ngRoute',
    NS.modules.application,
    NS.modules.facebook,
    NS.modules.infrastructure,
    NS.modules.list
]);

angular.module(NS.modules.application, []);
angular.module(NS.modules.facebook, []);
angular.module(NS.modules.infrastructure, []);
angular.module(NS.modules.list, []);

app.run(['$rootScope', '$window', 'AuthenticationService',
    function ($rootScope, $window, AuthenticationService) {

        $window.fbAsyncInit = function () {
            FB.init({
                appId: '822968334426637',
                status: true,
                cookie: true,
                xfbml: true,
                version: 'v2.4'
            });

            $rootScope.loading = false;
            $rootScope.isAuthenticated = false;
            $rootScope.user = {};

            AuthenticationService.checkLoginStatus();
        };

        (function (d) {
            var js,
                id = 'facebook-jssdk',
                ref = d.getElementsByTagName('script')[0];

            if (d.getElementById(id)) {
                return;
            }

            js = d.createElement('script');
            js.id = id;
            js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";

            ref.parentNode.insertBefore(js, ref);
        }(document));
    }]);