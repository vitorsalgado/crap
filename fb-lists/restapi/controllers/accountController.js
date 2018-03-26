var app = require('../app');
var express = require('express');
var authService = require('../services/authentication/authService');
var listService = require('../services/lists/listService');
var config = require('../infrastructure/config');
var authorize = require('../infrastructure/routing/authorize');
var router = express.Router();

router.get('/', authorize, function (req, res) {
    var userId = req.user.id;

    listService.getByUserId(userId)
        .then(function (response) {
            res.status(200).json(response);
        });
});

router.post('/', function (req, res) {
    var fbToken = req.body.fbToken;
    var appId = req.body.appId;

    if (!fbToken || !fbToken.length > 0 || !appId || !appId.length > 0) {
        res.status(400);
        return;
    }

    authService.authenticate(fbToken, appId)
        .then(function (response) {
            res.cookie('ssid', response.data.token, { maxAge: 262974383, httpOnly: true});
            res.status(200).json(response);
        })
        .fail(function (error) {
            res.status(500).json(error);
        });
});

app.use('/authentication', router);

module.exports = router;