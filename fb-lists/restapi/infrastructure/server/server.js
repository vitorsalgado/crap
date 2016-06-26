var http = require('http');
var app = require('../../app');
var port = process.env.PORT || 3000;

function Server() {
    this.start = function () {
        var server = http.createServer(app);

        server.listen(port, function () {
            console.log('http server is online and listening on port: ' + port);
        });

        server.on('error', onError);
        server.on('close', onClose);
    };

    function onError(error) {
        if (error.syscall !== 'listen') {
            throw error;
        }

        switch (error.code) {
            case 'EACCES':
                console.error('elevated privileges are required');
                process.exit(1);
                break;

            case 'EADDRINUSE':
                console.error(port + ' is already in use');
                process.exit(1);
                break;
            default:
                throw error;
        }
    }

    function onClose() {
        console.log('http server closed');
    }
}

module.exports = new Server();