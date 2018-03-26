var mongoose = require('mongoose');

var accountSchema = mongoose.Schema({
    username: {type: String, required: true, index: {unique: true}},
    email: {type: String, required: true},
    firstName: {type: String, required: true},
    lastName: {type: String, required: true},
    creationDate: {type: Date, 'default': Date.now},
    lastLogin: {type: Date, 'default': null},
    coverPictureUrl: {type: String, 'default': null},
    profilePictureUrl: {type: String, 'default': null},
    isActive: {type: Boolean, 'default': true},
    facebookUserId: {type: String, 'default': null}
});

accountSchema.methods.getFullName = function () {
    return (this.firstName + ' ' + this.lastName);
};

accountSchema.statics.findOrCreate = function (account, callback) {
    this.findOne({
        username: account.username,
        facebookUserId: account.facebookUserId
    }, function (error, foundAccount) {
        if (error) {
            callback(error);
            return;
        }

        if (!foundAccount) {
            account.save(function (saveError, ac) {
                callback(saveError, ac);
            });
        } else {
            callback(null, foundAccount);
        }
    });
};

module.exports = mongoose.model('Account', accountSchema);