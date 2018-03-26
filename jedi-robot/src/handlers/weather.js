var request = require('request');
var eventManager = require('../infrastructure/event/eventManager');

var expressions = [/jedi (weather)( me)? (.*)/i, /jedi (weather)( of)? (.*)/i, /jedi (clima|previsao)( de)? (.*)/i];
var conditions = [
    'tornado', 'tempestade tropical', 'furação', 'severe thunderstorms', 'thunderstorms',
    'mixed rain and snow', 'mixed rain and sleet', 'mixed snow and sleet', 'freezing drizzle', 'drizzle',
    'freezing rain', 'showers', 'showers', 'snow flurries', 'light snow showers',
    'blowing snow', 'snow', 'hail', 'sleet', 'dust', 'foggy',
    'haze', 'smoky', 'blustery', 'windy', 'cold', 'cloudy',
    'mostly cloudy (night)', 'mostly cloudy (day)', 'partly cloudy (night)', 'partly cloudy (day)', 'clear (night)', 'sunny',
    'Tempo claro (noite)', 'Tempo claro (dia)', 'mixed rain and hail', 'hot', 'isolated thunderstorms',
    'scattered thunderstorms', 'scattered thunderstorms', 'scattered showers', 'heavy snow', 'scattered snow showers',
    'heavy snow', 'partly cloudy', 'thundershowers', 'snow showers', 'isolated thundershowers'
];

conditions[3200] = 'Não disponível';

function WeatherHandler() {
    this.setUp = function () {
        eventManager.listen('message', function (message) {
            var hasAnyMatch = false;
            var cityQuery = '';

            for (var i = 0; i < expressions.length; i++) {
                var result = message.match(expressions[i]);

                if (result) {
                    cityQuery = result[3];
                    hasAnyMatch = true;
                    break;
                }
            }

            if (!hasAnyMatch) {
                return;
            }

            var woeidQuery = 'select * from geo.places(1) where text = "%' + cityQuery + '%"';
            var woeidUri = 'http://query.yahooapis.com/v1/public/yql?format=json&q=' + encodeURIComponent(woeidQuery);

            request({
                'uri': woeidUri
            }, function (error, response, body) {
                if (error) {
                    console.log('[weather handler error] > ' + error);
                    return;
                }

                var parsedBody = JSON.parse(body);
                var results = parsedBody.query.results;

                if (!results) {
                    eventManager.dispatch('send', 'Não encontramos nenhuma cidade para sua pesquisa :( ...');
                    return;
                }

                var woeid = results.place.woeid;
                var city = results.place.name;
                var country = results.place.country.content;

                var query = 'select item from weather.forecast where u = "c" and woeid = ' + woeid;
                var uri = 'http://query.yahooapis.com/v1/public/yql?format=json&u=c&q=' + encodeURIComponent(query);

                request({
                    'uri': uri
                }, function (error, response, body) {
                    if (error) {
                        eventEmitter.emit('error', '[weather handler error] > ' + error);
                        return;
                    }

                    var json = JSON.parse(body);
                    var item = json.query.results.channel.item;
                    var theWeather = '';

                    if (!item.condition) {
                        theWeather = item.title;
                    } else {
                        theWeather = city + ', ' + country + ', ' + item.condition.temp + '°C, ' + conditions[item.condition.code];
                    }

                    eventManager.dispatch('send', theWeather);
                });
            });
        });
    };

    this.setUpHelp = function () {
        eventManager.listen('help', function () {
            var help1 = '[clima de <city name>]\n';
            var help2 = '[weather of <city name>]                              Query weather conditions of specified place\n';
            var help3 = '[weather me <city name>]\n\n';

            eventManager.dispatch('send', help1);
            eventManager.dispatch('send', help2);
            eventManager.dispatch('send', help3);
        });
    };
}

module.exports = new WeatherHandler();