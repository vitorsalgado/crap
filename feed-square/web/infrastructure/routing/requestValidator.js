module.exports = function (req, res, next) {
    //if (req.isAuthenticated()) {
    //    return next();
    //}

    //res.redirect('/authentication/signin');
    //res.end();
    return next();
}
