var eventManager = require('../infrastructure/event/eventManager');
var request = require('request');

function Http() {
    var expressions = [/jedi http get ? (.*)/i, /jedi http post ? (.*)/i];

    this.setUp = function () {
        eventManager.listen('message', function (message) {
            var result;

            for (var i = 0; i < expressions.length; i++) {
                result = message.match(expressions[i]);

                if (result) break;
            }

            if (!result) {
                return;
            }

            var msg = result.toString().split(',')[0].split(' ');
            var cmd = msg[2];
            var act = msg[3].split(',')[0];

            actions[cmd](act);
        });
    };

    this.setUpHelp = function () {
        eventManager.listen('help', function () {
            var message = '[http get <url>]                                      Get http response information\n\n';
            eventManager.dispatch('send', message);
        });
    };

    function handleHttpResponse(response) {
        var msg = ''
                + 'content-type: ' + response.headers['content-type'] + '\n'
                + 'content-length: ' + response.headers['content-length'] + '\n'
                + 'cache-control: ' + response.headers['cache-control'] + '\n'
                + 'expires: ' + response.headers['expires'] + '\n'
                + 'last-modified: ' + response.headers['last-modified'] + '\n'
                + 'etag: ' + response.headers['etag'] + '\n'
                + 'date: ' + response.headers['date'] + '\n'
                + 'server: ' + response.headers['server'] + '\n'
            ;

        eventManager.dispatch('send', msg);
    }

    var actions = {
        'get': function (url) {
            request
                .get(url)
                .on('response', handleHttpResponse);
        }
    };
}

module.exports = new Http();