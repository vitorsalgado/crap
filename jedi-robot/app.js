#! /usr/bin/env node

var errorHandler = require('./src/infrastructure/error/errorHandler');
var robot = require('./src/robot');

errorHandler.configure();
robot.run();