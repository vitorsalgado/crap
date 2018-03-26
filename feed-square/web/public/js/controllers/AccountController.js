'use strict';

fq.controller('AccountController', ['$scope', '$q', function AccountController($scope, $q) {
    $scope.signIn = function () {
        var deferred = $q.defer();
        var options = {scope: 'email,user_likes,publish_actions', return_scopes: true};

        if (reAskForPermissions) {
            options.auth_type = 'rerequest';
        }

        FB.login(
            function (response) {
                if (response.authResponse && response.status == 'connected') {
                    deferred.resolve(response.authResponse);
                } else {
                    deferred.reject(response);
                }
            }, options
        );

        return deferred.promise;
    };

    $scope.signOut = function () {
        FB.logout(function (response) {
        });
    };
}]);

