angular
    .module(NS.modules.facebook)
    .service('PageService', PageService);

function PageService($q, $http) {

    this.getLikes = function () {
        var deferred = $q.defer();
        var fields = {fields: 'about,link,category,name,picture,description,likes'};

        FB.api('/me/likes?limit=100', fields, function (response) {
            if (response && response.data && response.data.length > 0) {
                if (response.paging && response.paging.next) {
                    recursivelyFetchPageResults(response.paging.next, response.data, function (pages) {
                        deferred.resolve(pages);
                    });
                }
            } else {
                deferred.reject();
            }
        });

        return deferred.promise;
    };

    function recursivelyFetchPageResults(pageUrl, pagesArray, callback) {
        $http({method: 'GET', url: pageUrl})
            .then(function (response) {
                var aux = pagesArray.concat(response.data);
                if (response.paging && response.paging.next) {
                    recursivelyFetchPageResults(response.paging.next, aux, callback);
                } else {
                    callback(aux);
                }
            });
    }
    
}