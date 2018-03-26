var app = require('../app');
var express = require('express');
var validateRequest = [require('../infrastructure/routing/requestValidator')];
var router = express.Router();

router.get('/', validateRequest, function (req, res) {
    res.render('main/index');
    res.end();
});

app.use('/', router);

module.exports = router;
