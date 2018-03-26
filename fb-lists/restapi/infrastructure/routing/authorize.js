var authService = require('../../services/authentication/authService');

function Authorize(req, res, next) {
    var token = ((req.cookies && req.cookies.ssid) || (req.body && req.body.access_token) || (req.query && req.query.access_token) || req.headers['x-access-token']);

    authService.authorize(token)
        .then(function (decoded) {
            req.user = {};
            req.user.id = decoded.userId;

            return next();
        })
        .fail(function () {
            res.status(401);
            res.end();
        });
}

module.exports = Authorize;