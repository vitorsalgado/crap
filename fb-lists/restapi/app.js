var express = require('express');
var path = require('path');
var bodyParser = require('body-parser');
var cookieParser = require('cookie-parser');

var app = module.exports = express();
var config = require('./infrastructure/config');

app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

app.use(bodyParser.json());
app.use(express.static(path.join(__dirname, 'public')));
app.use(cookieParser(config.security.secret));

var db = require('./persistence/db');
var server = require('./infrastructure/server/server');
var router = require('./infrastructure/routing/router');

db.start();
db.setUpModels();
router.setUp();
server.start();