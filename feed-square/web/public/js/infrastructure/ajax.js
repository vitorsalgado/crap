FQ.infrastructure.Ajax = (function () {
    return {
        postJSON: function (url, data) {
            var deferred = Q.defer();

            $.ajax({
                url: url,
                method: 'POST',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json',
                success: function (response) {
                    deferred.resolve(response);
                }
            });

            return deferred.promise;
        },

        getJSON: function (url, data) {
            var deferred = Q.defer();

            $.ajax({
                url: url,
                method: 'GET',
                data: data,
                dataType: 'json',
                contentType: 'application/json',
                success: function (response) {
                    deferred.resolve(response);
                }
            });

            return deferred.promise;
        }
    }
})();
