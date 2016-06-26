var app = require('../../app');
var express = require('express');
var jwt = require('jsonwebtoken');

var AuthenticationService = require('../../application/services/authenticationService');

var router = express.Router();

router.post('/facebook', function (req, res) {
    var fbToken = req.body.fbToken;
    var appId = req.body.appId;

    if (fbToken && fbToken.length > 0 && appId && appId.length > 0) {
        var authenticationService = new AuthenticationService();

        authenticationService.doFacebookSignIn(fbToken, appId)
            .then(function (loginResponse) {
                res.status(200).json(loginResponse);
            });
    } else {
        res.json(400, 'invalid credentials!');
    }
});

app.use('/api/v1/authentication/', router);

module.exports = router;
