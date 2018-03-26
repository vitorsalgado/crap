var jwt = require('jsonwebtoken');
var request = require('request');
var Q = require('q');
var Account = require('../../persistence/models/account');
var TokenPayload = require('./contracts/tokenPayload');
var config = require('../../infrastructure/config');
var LoginResponse = require('./contracts/loginResponse');
var Response = require('../response');

function AuthService() {

    this.authenticate = function (fbToken, appId) {
        var deferred = Q.defer();

        verifyFacebookAccessToken(fbToken)
            .then(function (incomingAccount) {
                Account.findOrCreate(incomingAccount, function (error, account) {
                    if (error) {
                        deferred.reject(error);
                        return;
                    }

                    var payload = new TokenPayload(account._id, account.facebookUserId, appId);
                    var token = jwt.sign(payload, config.security.secret, {
                        issuer: config.issuerName,
                        expiresIn: 31536000
                    });

                    deferred.resolve(new Response(new LoginResponse(account, token)));
                });
            })
            .fail(function (error) {
                deferred.reject(error);
            });

        return deferred.promise;
    };

    this.authorize = function (token) {
        var deferred = Q.defer();

        if (!token || token == '') {
            deferred.reject();
            return;
        }

        jwt.verify(token, config.security.secret, function (error, decoded) {
                if (error || !decoded) {
                    deferred.reject();
                } else {
                    deferred.resolve(decoded);
                }
            }
        );

        return deferred.promise;
    };

    this.getAuthUser = function (userId) {
        var deferred = Q.defer();

        Account.findOne({userId: userId}, function (error, foundAccount) {
            if (error) {
                deferred.reject(error);
                return;
            }

            deferred.resolve(foundAccount);
        });

        return deferred.promise;
    };

    function verifyFacebookAccessToken(accessToken) {
        var deferred = Q.defer();
        var fields = 'id,name,first_name,last_name,picture,about,cover,birthday,email,age_range';
        var url = 'https://graph.facebook.com/me?access_token=' + accessToken + '&fields=' + fields;

        request(url, function (error, response, body) {
            var data = JSON.parse(body);

            if (!error && response && response.statusCode && response.statusCode == 200) {
                var user = {
                    facebookUserId: data.id,
                    username: data.email,
                    firstName: data.first_name,
                    lastName: data.last_name,
                    coverPictureUrl: data.cover.source,
                    profilePictureUrl: data.picture.data.url,
                    email: data.email
                };

                deferred.resolve(user);
            } else {
                console.log(data.error);
                deferred.reject({code: response.statusCode, message: data.error.message});
            }
        });

        return deferred.promise;
    }

}

module.exports = new AuthService();