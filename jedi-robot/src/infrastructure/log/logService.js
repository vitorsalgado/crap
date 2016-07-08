var fs = require('fs');

function LogService() {
    var logFile = './log/log.txt';

    this.log = function (data) {
        try {
            doLog(data);
        } catch (ex) {
        }
    };

    function doLog(data) {
        var fileBuffer = fs.readFileSync(logFile);
        var lines = fileBuffer.toString().split("\n");

        if (lines.length - 1 > 100) {
            fs.writeFile(logFile, '', function (error) {
                if (!error) {
                    writeLog(data);
                }
            });
        } else {
            writeLog(data);
        }
    }

    function writeLog(data) {
        var date = new Date();
        var content = "# Date: {" + date.toUTCString() + '} | Content: ' + data + '\r\n';

        fs.appendFile(logFile, content, 'utf8', function (error) {
        });
    }
}

module.exports = new LogService();