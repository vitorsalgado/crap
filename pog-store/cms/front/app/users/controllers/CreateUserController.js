angular
    .module('br.com.pogstore.cms.users')
    .controller('CreateUserController', CreateUserController);

function CreateUserController($scope, $modalInstance) {
    $scope.save = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };
}
