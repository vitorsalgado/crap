module.exports = {
    db: 'mongodb://localhost/fb-lists',
    issuerName: 'fblists.io',
    security: {
        secret: 'dasdnkjasbdjadbjsadsajdbsadakndj',
        cookieDomain: '',
        cookieName: 'ssid',
        cookiePath: '/'
    },
    facebook: {
        clientID: "822968334426637", //process.env.FACEBOOK_CLIENTID,
        clientSecret: "3fc50cebf94489da05cadc0f2e17352c", //process.env.FACEBOOK_SECRET,
        callbackURL: "http://localhost:3000/authentication/facebook/callback"
    },
    applicationId: "fb-lists-main-app"
};
