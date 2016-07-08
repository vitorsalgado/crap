var util = require('util');
var events = require('events');

function EventManager() {
    var eventEmitter = new events.EventEmitter();

    events.EventEmitter.call(this);

    this.listen = function (event, callback) {
        eventEmitter.on(event, callback);
    };

    this.dispatch = function (event, data) {
        eventEmitter.emit(event, data);
    };
}

util.inherits(EventManager, events.EventEmitter);

module.exports = new EventManager();