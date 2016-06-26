'use strict';

angular
    .module('br.com.pogstore.cms.users')
    .controller('UserController', UserController);

function UserController($scope, $modal) {
    $scope.openNewWindow = function () {
        var modalInstance = $modal.open({
            templateUrl: 'app/users/_create.html',
            controller: 'CreateUserController'
        });

        modalInstance.result.then(function () {
            //save
        }, function () {
            //cancel
        });
    };
}