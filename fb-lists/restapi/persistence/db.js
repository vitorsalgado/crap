var fs = require('fs');
var mongoose = require('mongoose');
var config = require('../infrastructure/config');
var app = require('../app');

function Db() {
    this.start = function () {
        connect();

        mongoose.connection.on('error', console.log);
        // TODO: find a better way
        //mongoose.connection.on('disconnected', connect);
    };

    this.setUpModels = function () {
        fs.readdirSync('./persistence/models/')
            .forEach(function (file) {
                require('./models/' + file.replace('.js', ''));
            });
    };

    function connect() {
        mongoose.connect(config.db, {server: {socketOptions: {keepAlive: 1}}});
        console.log('mongoose is online and listening on: ' + config.db);
    }
}

module.exports = new Db();