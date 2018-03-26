"use strict";

const url = require('url')
    , localeTb = require('../localeRoutes');

module.exports = (req, res, next) => {
    let incoming_culture = req.path.split('/')[1];
    let app = req.app;

    if (!['pt', 'en', 'es'].some(x => x == incoming_culture)) {
        incoming_culture = 'pt';
    }

    let query_separator_index = req.url.indexOf('?');
    let query = '';

    if (query_separator_index > -1) {
        query = req.url.substr(req.url.indexOf('?'), req.url.length - 1);
    }

    req.culture = incoming_culture;

    res.locals.routes = {pt: '/', en: '/en', es: '/es'};
    res.locals.routeTb = localeTb;
    res.locals.Res = app.locals.res;
    res.locals.Res.culture = incoming_culture;

    next();
};