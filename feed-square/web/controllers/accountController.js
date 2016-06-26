var app = require('../app');
var express = require('express');
var request = require('request');
var config = require('../infrastructure/config');

var router = express.Router();

router.get('/signin', function (req, res) {
    res.render('account/signin');
});

router.post('/signin', function (req, res) {
    var apiUrl = config.apiBaseUrl + 'authentication/facebook/';
    var data = req.body;

    data.appId = config.applicationId;

    request(
        {url: apiUrl, method: 'POST', json: data},
        function (error, response, body) {
            if (!error && response.statusCode == 200) {
                res.cookie(config.security.cookieName, body.token,
                    { domain: config.security.cookieDomain, path: '/', secure: true, maxAge: 262974383 });
                res.status(200).json(body);
            } else {
                console.log(error);
                res.write(error);
            }
        }
    );
});

router.post('/signout', function (req, res) {
    req.logout();
    res.redirect('/authentication/signin');
});

app.use('/', router);

module.exports = router;
