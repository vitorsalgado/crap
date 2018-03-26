window.CrimeMap = window.CrimeMap || {};
CrimeMap.location = CrimeMap.CrimeMap || {};

CrimeMap.location.getUserLocation = function (callback) {

    var defaultCoords = {
        latitude: -23.544945,
        longitude: -46.634564
    };

    if (navigator.geolocation) {

        navigator.geolocation.getCurrentPosition(
            function (point) {
                var userCoords = {
                    latitude: point.coords.latitude,
                    longitude: point.coords.longitude
                };
                callback(userCoords);
            },
            function(){
                callback(defaultCoords);
            }
        );

    } else {
        callback(defaultCoords);
    }
}
