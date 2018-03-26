var fs = require('fs');
var app = require('../../app');

function Router() {
    this.setUp = function () {
        fs.readdirSync('./controllers/')
            .forEach(function (controller) {
                require('../../controllers/' + controller.replace('.js', ''))
            });
    };
}

module.exports = new Router();