var fs = require('fs');
var jwt = require('jsonwebtoken');

module.exports.setUpControllers = function () {
    var controllers = fs.readdirSync('./api/controllers/');

    for (var i = 0; i < controllers.length; i++) {
        var controller = controllers[i].replace('.js', '');
        require('../../api/controllers/' + controller);
    }
};

module.exports.setUpRoutes = function (app) {
    app.all('/*', function (req, res, next) {
        res.header("Access-Control-Allow-Origin", "*");
        res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE,OPTIONS');
        res.header('Access-Control-Allow-Headers', 'Content-type,Accept,X-Access-Token,X-Key');

        if (req.method == 'OPTIONS') {
            res.status(200).end();
        } else {
            next();
        }
    });

    app.all('/api/v1/*', validateRequest);
};

module.exports.validateRequest = function (req, res, next) {
    var token = req.headers['x-access-token'];

    if(!token ){
        return res.status(401).json({
            success: false,
            message: 'No token provided.'
        });
    }

    jwt.verify(token, app.get('superSecret'), function(error, decoded) {
        if (error) {
            return res.json({ success: false, message: 'Failed to authenticate token.' });
        } else {
            req.decoded = decoded;
            next();
        }
    });
};