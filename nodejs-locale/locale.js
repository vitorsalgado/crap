"use strict";

const fs = require('fs')
    , xml2js = require('xml2js');

let res = require('./res');

module.exports = (app) => {
    return new Promise((resolve) => {
        fs.readFile('./config/locale/res.xml', {encoding: 'utf-8'}, (error, data) => {
            xml2js.parseString(data, (err, result) => {
                let resources = {};

                result.resources.data.forEach(element => {
                    let name = element.$.name;

                    resources[name] = {};
                    resources[name]['pt'] = element.pt ? element.pt.toString() : '';
                    resources[name]['en'] = element.en ? element.en.toString() : '';
                    resources[name]['es'] = element.es ? element.es.toString() : '';

                    res[name] = () => app.locals.getResource(`${name}`, res.culture);
                });

                res.data = resources;
                res.get = (key) => app.locals.getResource(key, res.culture);

                app.set('resources', resources);
                app.locals.res = res;

                app.locals.getResource = (key, language) => {
                    let resources = app.get('resources');

                    if (!resources) {
                        return '';
                    }

                    let resource = resources[key];

                    if (!resource) {
                        return '<<TBD>>';
                    }

                    if (!language) {
                        language = 'pt';
                    }

                    let result;

                    switch (language) {
                        case 'pt':
                            result = resource.pt;
                            break;
                        case 'en':
                            result = resource.en;
                            break;
                        case 'es':
                            result = resource.es;
                            break;
                        default:
                            return resource.pt;
                    }

                    return result ? result : resource.pt;
                };

                resolve();
            });
        });
    });
};