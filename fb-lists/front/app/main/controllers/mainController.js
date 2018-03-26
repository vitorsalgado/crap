angular
    .module(NS.modules.application)
    .controller('MainController', MainController);

function MainController($scope, $rootScope, $uibModal, $window, $timeout, ListService) {
    $scope.list = {};

    $scope.login = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'app/account/views/login.html',
            controller: 'LoginController',
            size: 'sm'
        });

        modalInstance.result.then(function () {
            //
        }, function () {
            // cancel action - do nothing
        });
    };

    $scope.createList = function () {
        var modalInstance1 = $uibModal.open({
            templateUrl: '/app/list/views/create1.html',
            controller: 'CreateListController1'
        });

        modalInstance1.result.then(function (list) {
            $scope.list = list;

            $timeout(function () {
                var modalInstance2 = $uibModal.open({
                    templateUrl: '/app/list/views/create2.html',
                    controller: 'CreateListController2',
                    size: 'lg',
                    resolve: {
                        list: function () {
                            return $scope.list;
                        }
                    }
                });

                modalInstance2.result.then(function (response) {
                    $window.location.href = '/list/' + response.data.data.id + '/' + response.data.data.name.toLowerCase();
                });
            }, 500);
        });
    };

    $scope.getMyLists = function () {
        ListService.getAllByUser($rootScope.user.id);
    };
}