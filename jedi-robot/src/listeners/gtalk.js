var config = require('../infrastructure/config');
var eventManager = require('../infrastructure/event/eventManager');

function GtalkAdapter() {
    var self = this;
    var google = config.bots.google;
    var client = null;
    var xmpp = null;

    self.name = 'Gtalk';

    self.setUp = function () {
        xmpp = require('node-xmpp');

        createGoogleClient();

        eventManager.listen('send', sendMessage);

        eventManager.dispatch('initialized', self.name);
    };

    function createGoogleClient() {
        client = new xmpp.Client({
            reconnect: true,
            jid: google.jid,
            password: google.password,
            host: google.host,
            port: google.port
        });

        client.on('online', function () {
            client.send(new xmpp.Element('presence'));

            var roster_query = new xmpp.Element('iq', {
                type: 'get',
                id: (new Date()).getTime()
            }).c('query', {
                xmlns: 'jabber:iq:roster'
            });

            client.send(roster_query);

            setInterval(function () {
                client.send(' ');
            }, google.keepAliveInterval);
        });

        client.on('error', function (error) {
            eventManager.dispatch('fail', '[xmpp error] > ' + error);
        });

        client.on('stanza', function (stanza) {
            var from = stanza.attrs.from;

            if (stanza.attrs.type === 'error') {
                eventManager.dispatch('fail', 'xmpp stanza] > ' + stanza.toString());
                return;
            }

            if (from == google.username)
                return;

            if (stanza.is('message' || ((_ref1 = stanza.attrs.type) !== 'groupchat' && _ref1 !== 'direct' && _ref1 !== 'chat'))) {

                var body = stanza.getChild('body');

                if (!body) {
                    return;
                }

                eventManager.dispatch('messageIn', body.getText());
            }
        });
    }

    function sendMessage(message) {
        var messageElement = new xmpp.Element('message', {
            from: client.jid.toString(),
            to: to,
            type: 'chat'
        }).c('body').t(message);

        client.send(messageElement);
    }
}

module.exports = new GtalkAdapter();