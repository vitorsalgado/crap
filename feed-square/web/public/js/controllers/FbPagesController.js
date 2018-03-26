'use strict';

fq.controller('FbPagesController', ['$scope', '$q', '$http', function FbPagesController($scope, $q, $http) {
    $scope.getPageLikes = function () {
        var deferred = $q.defer();
        var fields = {fields: 'about,link,category,name'};

        FB.api('/me/likes?limit=100', fields, function (response) {
            if (response && response.data && response.data.length > 0) {
                if (response.paging && response.paging.next) {
                    recursivelyFetchPageResults($http, response.paging.next, response.data, function (pages) {
                        deferred.resolve(pages);
                    });
                }
            } else {
                deferred.reject();
            }
        });

        return deferred.promise;
    };

    function recursivelyFetchPageResults($http, pageUrl, pagesArray, callback) {
        $http.get(pageUrl)
            .success(function (response) {
                var aux = pagesArray.concat(response.data);
                if (response.paging && response.paging.next) {
                    recursivelyFetchPageResults(response.paging.next, aux, callback);
                } else {
                    callback(aux);
                }
            });
    }
}]);
