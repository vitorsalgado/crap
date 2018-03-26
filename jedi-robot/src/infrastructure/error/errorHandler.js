function ErrorHandler() {
    this.configure = function () {
        process.on('uncaughtException', function (error) {
            console.log(error);
        });
    };
}

module.exports = new ErrorHandler();