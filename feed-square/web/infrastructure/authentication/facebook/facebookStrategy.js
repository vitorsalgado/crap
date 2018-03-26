var request = require('request');
var FacebookStrategy = require('passport-facebook').Strategy;
var config = require('../../../infrastructure/config');

module.exports = new FacebookStrategy({
        clientID: config.facebook.clientID,
        clientSecret: config.facebook.clientSecret,
        callbackURL: config.facebook.callbackURL
    },
    function (accessToken, refreshToken, profile, done) {
    	var url = 'http://localhost:3001/api/v1/authentication/sign/?fbToken=' + accessToken;
        console.log(accessToken);
        request(url, function (error, response, body) {
            if (!error && response.statusCode == 200) {
                console.log(body);
            }else{
                console.log('error on request' + error);
            }
        });

        return done(null, profile);
    }
);
