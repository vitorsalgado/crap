var eventManager = require('../infrastructure/event/eventManager');
var readline = require('readline');

function ShellAdapter() {
    var self = this;
    var rl = readline.createInterface(process.stdin, process.stdout, null);
    var promptPrefix = 'jedi > ';
    var promptLength = promptPrefix.length;
    var interval = null;

    self.name = "Shell";

    self.setUp = function () {
        eventManager.listen('send', sendMessage);

        process.title = 'JEDI Robot Console Listener';
        process.stdin.setEncoding('utf8');
        process.stdout.write('# Welcome to JEDI Robot Console Listener -|----------- \n\n');

        setPrompt();

        rl.on('line', function (data) {
            eventManager.dispatch('messageIn', data);
            setPrompt();
        });

        eventManager.dispatch('initialized', self.name);
    };

    function sendMessage(message) {
        setPrompt();
        rl.write(message);
    }

    function setPrompt() {
        flush(function () {
            rl.setPrompt(promptPrefix, promptLength);
            rl.prompt();
        });
    }

    function flush(callback) {
        if (process.stdout.write('')) {
            callback();
        } else {
            process.stdout.once('drain', function () {
                callback();
            });
        }
    }
}

module.exports = new ShellAdapter();