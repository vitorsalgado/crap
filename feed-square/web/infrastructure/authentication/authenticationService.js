var app = require('../../app');
var passport = require('passport');
var session = require('express-session');

var facebookStrategy = require('./facebook/facebookStrategy');
var config = require('../../infrastructure/config');

app.use(session({secret: config.session_secret}));
app.use(passport.initialize());
app.use(passport.session());

passport.serializeUser(function (user, done) {
    done(null, user);
});

passport.deserializeUser(function (obj, done) {
    done(null, obj);
});

passport.use(facebookStrategy);
