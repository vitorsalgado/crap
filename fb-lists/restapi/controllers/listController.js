var app = require('../app');
var express = require('express');
var lstService = require('../services/lists/listService');
var authorize = require('../infrastructure/routing/authorize');

var router = express.Router();

router.post('/', authorize, function (req, res) {
    lstService.add(req.body)
        .then(function (obj) {
            res.status(200).json(obj);
        })
        .fail(function (error) {
            res.status(500).json(error);
        });
});

router.get('/user/:userId/', authorize, function (req, res) {
    lstService.getByUserId(req.params.userId)
        .then(function (response) {
            res.status(200).json(response);
        })
        .fail(function (error) {
            res.status(500).json(error);
        });
});

router.get('/:id/:name', authorize, function (req, res) {
    lstService.get(req.params.id)
        .then(function (list) {
            if (list.name.toLowerCase() != req.params.name) {
                res.redirect(301, '/list/' + list.id + '/' + list.name.toLowerCase());
            } else {
                res.render('list/index', list);
            }
        });
});

app.use('/list', router);

module.exports = router;