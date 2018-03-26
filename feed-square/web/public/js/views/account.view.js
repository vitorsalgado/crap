(function () {
    var loginButton = document.querySelector('#btn-facebook-login');

    loginButton.addEventListener('click', function () {
        var accountController = FQ.controllers.AccountController();

        accountController.signIn(false)
            .then(function () {
                FQ.infrastructure.ViewHelper.redirectToHome();
            });
    });
})();
