var mongoose = require('mongoose');

var accountSchema = mongoose.Schema({
    username: {type: String, required: true, index: {unique: true}},
    email: {type: String, required: true},
    firstName: {type: String, required: true},
    lastName: {type: String, required: true},
    creationDate: {type: Date, 'default': Date.now},
    lastLogin: {type: Date, 'default': null},
    isActive: {type: Boolean, 'default': true},
    facebookUserId: {type: String, 'default': null}
});

accountSchema.methods.getFullName = function() {
    return (this.firstName + ' ' + this.lastName);
};

var Account = mongoose.model('Account', accountSchema);

module.exports = Account;
