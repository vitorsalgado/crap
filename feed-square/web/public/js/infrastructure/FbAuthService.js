FQ.infrastructure.FbAuthService = (function () {
    return {
        checkLoginStatus: function () {
            var deferred = Q.defer();

            FB.getLoginStatus(function (response) {
                if (response.status === 'connected') {
                    connectedCallback(response);
                    deferred.resolve();
                } else {
                    deferred.reject();
                }
            });

            return deferred.promise;
        },

        listenToFbAuthChanges: function (onConnected, onNotConnected) {
            FB.Event.subscribe('auth.authResponseChange', function (response) {
                if (response.status === 'connected') {
                    connectedCallback(response);
                    onConnected();
                } else {
                    notConnectedCallback();
                    onNotConnected();
                }
            });
        }
    };

    function connectedCallback(response) {
        var fields = {fields: 'id,name,picture,about,cover,birthday,email,age_range'};

        FB.api('me/', fields, function (response) {
            if (response) {
                var globals = FQ.infrastructure.Globals;

                globals.user.id = response.id;
                globals.user.coverPicture = response.cover.source;
                globals.user.picture = response.picture.data.url;
                globals.user.name = response.name;
                globals.user.email = response.email;
            }
        });
    }

    function notConnectedCallback() {
        var globals = FQ.infrastructure.Globals;

        globals.user.id = '';
        globals.user.coverPicture = '';
        globals.user.picture = '';
        globals.user.name = '';
        globals.user.email = '';
    }

})();
