$(document).ready(function () {
    function facebookReady() {
        FB.init({
            appId: FQ.infrastructure.Settings.facebook.appId,
            status: true,
            xfbml: true,
            cookie: true,
            version: FQ.infrastructure.Settings.facebook.apiVersion
        });

        FQ.infrastructure.EventManager.trigger(document, 'facebook:ready');
    }

    if (window.FB) {
        facebookReady();
    } else {
        window.fbAsyncInit = facebookReady;
    }

    FQ.infrastructure.EventManager.listen(document, 'facebook:ready', function () {
        FQ.infrastructure.FbAuthService.listenToFbAuthChanges();



        FQ.infrastructure.FbAuthService.checkLoginStatus()
            .then(function () {
                FQ.infrastructure.ViewHelper.hideLoadingOverlay();
            });
    });

    FQ.views.Main.init();
});
