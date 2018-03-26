var express = require('express');
var path = require('path');
var bodyParser = require('body-parser');
var mongoose = require('mongoose');

var app = module.exports = express();

app.set('views', path.join(__dirname, 'api/views'));
app.set('view engine', 'jade');

app.use(bodyParser.json());
app.use(express.static(path.join(__dirname, 'public')));

var server = require('./infrastructure/server/server');
var router = require('./infrastructure/routing/routes');
var errorHandler = require('./infrastructure/server/errorHandler');
var dbConfig = require('./infrastructure/db/dbConfig');

router.setUpControllers();
router.setUpRoutes(app);
server.startHttpServer(app);
errorHandler.setUpErrorHandlers(app);
dbConfig.setUpDb();
dbConfig.setUpModels();
