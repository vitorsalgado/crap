var http = require('http');

module.exports.startHttpServer = function (app) {
    var port = process.env.PORT || 3000;
    var server = http.createServer(app);

    app.set('port', port);

    server.listen(port, function () {
        var addr = server.address();
        var bind = typeof addr === 'string'
            ? 'pipe ' + addr
            : 'port ' + addr.port;

        console.log('feed square web client is online and listening on ' + bind);
    });

    server.on('error', onError);
}

function onError(error) {
    if (error.syscall !== 'listen') {
        throw error;
    }

    var bind = 'BIND WEB - ';
    //var bind = typeof port === 'string'
    //    ? 'Pipe ' + port
    //    : 'Port ' + port;

    // handle specific listen errors with friendly messages
    switch (error.code) {
        case 'EACCES':
            console.error(bind + ' requires elevated privileges');
            process.exit(1);
            break;
        case 'EADDRINUSE':
            console.error(bind + ' is already in use');
            process.exit(1);
            break;
        default:
            throw error;
    }
}
