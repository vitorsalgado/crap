var fs = require('fs');
var logService = require('./infrastructure/log/logService');
var eventManager = require('./infrastructure/event/eventManager');

function Robot() {

    var handlerCount = 0;

    this.run = function () {
        eventManager.listen('messageIn', handleIncomingMessage);
        eventManager.listen('fail', handleError);

        setUpAdapters();
        setUpHandlers();
    };

    function handleIncomingMessage(message) {
        if (message === null || message === '')
            return;

        isHelpRequest(message)
            ? eventManager.dispatch('help')
            : eventManager.dispatch('message', message);
    }

    function handleError(error) {
        if (typeof error == 'string') {
            eventManager.dispatch('send', error);
        }
    }

    function setUpAdapters() {
        fs.readdirSync('./src/listeners')
            .forEach(function (file) {
                var adapter = require('./listeners/' + file);

                try {
                    adapter.setUp();
                } catch (ex) {
                    logService.log('Error loading adapter "' + adapter.name);
                }
            });
    }

    function setUpHandlers() {
        fs.readdirSync('./src/handlers')
            .forEach(function (file) {
                var handler = require('./handlers/' + file);

                handler.setUp();
                handler.setUpHelp();

                handlerCount++;
            });
    }

    function isHelpRequest(message) {
        return [/jedi (help)( me)? (.*)/i, /jedi help/i, /ajuda/i]
            .some(function (x) {
                return message.match(x);
            });
    }
}

module.exports = new Robot();