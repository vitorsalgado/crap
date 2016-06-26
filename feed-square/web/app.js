var express = require('express');
var path = require('path');
var bodyParser = require('body-parser');
var cookieParser = require('cookie-parser');

var config = require('./infrastructure/config');

var app = module.exports = express();

app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

app.use(bodyParser.json());
app.use(cookieParser(config.security.secret));
app.use(express.static(path.join(__dirname, 'public')));

var server = require('./infrastructure/server/server');
var router = require('./infrastructure/routing/router');
var errorHandler = require('./infrastructure/server/errorHandler');

router.setUpControllers();
router.setUpRoutes(app);
server.startHttpServer(app);
errorHandler.setUpErrorHandlers(app);
