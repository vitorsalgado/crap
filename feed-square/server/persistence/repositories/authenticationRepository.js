var Q = require('q');
var Account = require('../models/authentication/account');

var AccountRepository = function () {
    this.findOrCreate = function (account) {
        var deferred = Q.defer();

        findAccountByUsername(account.email)
            .then(function (foundAccount) {
                if (foundAccount) {
                    deferred.resolve(foundAccount);
                } else {
                    var newAccount = new Account({
                        username: account.email,
                        email: account.email,
                        firstName: account.firstName,
                        lastName: account.lastName,
                        lastLogin: new Date(),
                        facebookUserId: account.facebookUserId
                    });

                    newAccount.save(function (error, createdAccount) {
                        if (error) {
                            deferred.reject(new Error(error));
                        } else {
                            deferred.resolve(createdAccount);
                        }
                    });
                }
            })
            .fail(function (error) {
                deferred.reject(error);
            });

        return deferred.promise;
    };

    this.saveSecurityToken = function(securityToken){
        var deferred = Q.defer();

        securityToken.save(function (error, securityToken) {
            if (error) {
                console.log(error);
                deferred.reject(error);
            }

            deferred.resolve(securityToken);
        });

        return deferred.promise;
    };
};

function findAccountByUsername(username) {
    var deferred = Q.defer();

    Account.findOne({email: username}, function (err, user) {
        if (err) {
            deferred.reject(new Error(err));
        } else {
            deferred.resolve(user);
        }
    });

    return deferred.promise;
}

module.exports = AccountRepository;
