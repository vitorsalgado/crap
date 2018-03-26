angular
    .module(NS.modules.application)
    .controller('LoginController', LoginController);

function LoginController($scope, $uibModalInstance, AuthenticationService) {
    $scope.signIn = function () {
        AuthenticationService.signIn(false).then(
            function (response) {
                $uibModalInstance.close();
            },
            function (error) {
                console.log(error);
            }
        )
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    };
}
