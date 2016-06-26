angular
    .module(NS.modules.list)
    .service('ListService', ListService);

function ListService($http, $q) {
    this.add = function (list) {
        var deferred = $q.defer();

        $http({method: 'POST', url: '/list', data: list}).then(function (response) {
            deferred.resolve(response);
        });

        return deferred.promise;
    };

    this.getAllByUser = function (userId) {
        $http({method: 'GET', url: '/list/user/' + userId}).then(function (response) {

        });
    }
}