var fs = require('fs');
var mongoose = require('mongoose');
var config = require('../config');
var app = require('../../app');

module.exports = {
    setUpDb: function () {
        var connect = function () {
            var options = {server: {socketOptions: {keepAlive: 1}}};
            mongoose.connect(config.db, options);
        };

        connect();

        mongoose.connection.on('error', console.log);
        mongoose.connection.on('disconnected', connect);
    },

    setUpModels: function () {
        var accountModels = fs.readdirSync('./persistence/models/authentication/');
        for (var i = 0; i < accountModels.length; i++)
            require('../../persistence/models/authentication/' + accountModels[i].replace('.js', ''));
    }
};
