var mongoose = require('mongoose');

var securityTokenSchema = mongoose.Schema({
    apiAccessToken: {type: String, required: true, index: {unique: true}},
    issuedDate: {type: Date, required: true},
    application: {type: String, required: true},
    userId: {type: [mongoose.Schema.ObjectId], required: true},
    facebookAccessToken: {type: String, required: true}
});

var SecurityToken = mongoose.model('SecurityToken', securityTokenSchema);

module.exports = SecurityToken;
