var app = require('../../app');
var express = require('express');
var request = require('request');

var router = express.Router();
//100001103617763
router.get('/list', function (req, res) {
    request('https://graph.facebook.com/me/likes/?access_token=' + req.query.access_token, function(error, response, body){
        if (!error && response.statusCode == 200) {
            console.log(body);
        }else{
            console.log('error on request ' + response);
        }

        res.write(req.query.access_token);
    });
    res.end();
});

app.use('/api/v1/pages/', router);

module.exports = router;
