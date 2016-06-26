angular
    .module(NS.modules.application)
    .service('AuthenticationService', AuthenticationService);

function AuthenticationService($q, $http, $rootScope, DataStorage) {

    const FB_CONNECTED = 'connected';

    this.watchAuthenticationStatusChanges = function () {
        FB.Event.subscribe('auth.authResponseChange', function (response) {
            if (response.status === FB_CONNECTED) {
                connectedCallback(response);
            } else {
                notConnectedCallback();
            }
        });
    };

    this.checkLoginStatus = function () {
        FB.getLoginStatus(function (response) {
            if (response.status === FB_CONNECTED) {
                connectedCallback(response);
            } else {
                notConnectedCallback(response);
            }
        });
    };

    this.signIn = function (reAskForPermissions) {
        var deferred = $q.defer();
        var options = {scope: 'email,user_likes,publish_actions', return_scopes: true};

        if (reAskForPermissions) {
            options.auth_type = 'rerequest';
        }

        FB.login(
            function (response) {
                if (response.authResponse && response.status == FB_CONNECTED) {
                    connectedCallback(response);
                    deferred.resolve(response.authResponse);
                } else {
                    deferred.reject(response);
                }
            }, options
        );

        return deferred.promise;
    };

    this.signOut = function () {
        var deferred = $q.defer();

        FB.logout(function (response) {
            notConnectedCallback();
            deferred.resolve(response);
        });

        return deferred.promise;
    };

    function connectedCallback(response) {
        var fields = {fields: 'id,name,picture,about,cover,birthday,email,age_range'};

        FB.api('me/', fields, function (meResponse) {
            var req = {
                method: 'POST', url: '/authentication/facebook',
                data: {fbToken: response.authResponse.accessToken, appId: 'fb-lists-webclient'}
            };

            $http(req).then(function (authResponse) {
                setRootScope(authResponse.data);
            });
        });
    }

    function notConnectedCallback() {
        $rootScope.user.id = '';
        $rootScope.user.coverPicture = '';
        $rootScope.user.picture = '';
        $rootScope.user.name = '';
        $rootScope.user.email = '';

        $rootScope.isAuthenticated = false;

        DataStorage.removeItem(Global.consts.USER_KEY);
        DataStorage.removeItem(Global.consts.ACCESS_TOKEN);
    }

    function setRootScope(authResponse) {
        var data = authResponse.data;
        var account = data.account;

        $rootScope.isAuthenticated = true;
        $rootScope.user.id = account._id;
        $rootScope.user.coverPicture = account.profilePictureUrl;
        $rootScope.user.picture = account.coverPictureUrl;
        $rootScope.user.name = account.firstName + ' ' + account.lastName;
        $rootScope.user.fbId = account.facebookUserId;

        DataStorage.setItem(Global.consts.USER_KEY, $rootScope.user);
        DataStorage.setItem(Global.consts.ACCESS_TOKEN, data.token);
    }
}