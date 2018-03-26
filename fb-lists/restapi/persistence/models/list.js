var mongoose = require('mongoose');

var schema = mongoose.Schema({
    userId: {type: String, required: true},
    name: {type: String, required: true},
    description: {type: String, required: true},
    tags: {type: String, required: false},
    likes: {type: Number, required: false, 'default': 0},
    pages: {type: Array, required: false},
    creationDate: {type: Date, 'default': Date.now}
});

var List = mongoose.model('List', schema);

module.exports = List;