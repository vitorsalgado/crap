module.exports = {
    facebook: {
        clientID: "822968334426637", //process.env.FACEBOOK_CLIENTID,
        clientSecret: "3fc50cebf94489da05cadc0f2e17352c", //process.env.FACEBOOK_SECRET,
        callbackURL: "http://localhost:3000/authentication/facebook/callback"
    },
    session_secret: "tempsecret",
    apiBaseUrl: "http://localhost:3001/api/v1/",
    applicationId: "feed-square-web-client",
    security: {
        secret: 'dsadadadasdasdadasndkajsdnasjdnk',
        cookieDomain: '',
        cookieName: '',
        cookiePath: '/'
    }
};
