var request = require('request');
var Q = require('q');
var jwt = require('jsonwebtoken');

var AuthenticationRepository = require('../../persistence/repositories/authenticationRepository');
var ApiAccessTokenPayload = require('../../infrastructure/security/apiAccessTokenPayload');
var SecurityToken = require('../../persistence/models/authentication/securityToken');
var AccountDto = require('../dtos/accountDto');
var FacebookLoginResponse = require('../messages/facebookLoginResponse');
var config = require('../../infrastructure/config');

var AuthenticationService = function () {

    this.doFacebookSignIn = function (facebookToken, applicationName, userAgent) {
        var deferred = Q.defer();
        var authenticationRepository = new AuthenticationRepository();

        verifyFacebookAccessToken(facebookToken)
            .then(function (profile) {

                authenticationRepository.findOrCreate(profile)
                    .then(function (account) {
                        if (profile.facebookUserId != account.facebookUserId) {
                            deferred.reject(new Error('Invalid Token!'));
                        } else {
                            var payload = new ApiAccessTokenPayload(account._id, account.facebookUserId, applicationName);
                            var token = jwt.sign(payload, config.security.secret, {
                                issuer: config.issuerName,
                                expiresInMinutes: 525948766
                            });

                            var securityToken = createSecurityToken(token, facebookToken, payload);

                            authenticationRepository.saveSecurityToken(securityToken)
                                .then(function (securityToken) {
                                    var accountDto = new AccountDto(
                                        account.username, account.firstName, account.lastName);

                                    var response = new FacebookLoginResponse(accountDto, token);

                                    deferred.resolve(response);
                                });
                        }
                    })
                    .fail(function (error) {
                        console.log(error);
                        deferred.reject(error);
                    });
            });

        return deferred.promise;
    };

    this.authorize = function (userId, apiAccessToken, callback) {
        var query = {apiAccessToken: apiAccessToken};

        SecurityToken.findOne(query, function (error, securityToken) {
                if (error) {
                    callback(new Error(error));
                } else {
                    if (securityToken !== null && !securityToken.isExpired() && securityToken.userId.toString() === userId.toString()) {
                        callback(true);
                    } else {
                        callback(false);
                    }
                }
            }
        );
    };

};

function verifyFacebookAccessToken(accessToken) {
    var deferred = Q.defer();
    var url = 'https://graph.facebook.com/me?access_token=' + accessToken;

    request(url, function (error, response, body) {
        var data = JSON.parse(body);

        if (!error && response && response.statusCode && response.statusCode == 200) {
            var user = {
                facebookUserId: data.id,
                username: data.username,
                firstName: data.first_name,
                lastName: data.last_name,
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

function createSecurityToken(token, facebookToken, payLoad) {
    var now = new Date();
    var obj = new SecurityToken();

    obj.apiAccessToken = token;
    obj.issuedDate = now.toJSON();
    obj.application = payLoad.application;
    obj.userId = payLoad.userId;
    obj.facebookAccessToken = facebookToken;

    return obj;
}

module.exports = AuthenticationService;
