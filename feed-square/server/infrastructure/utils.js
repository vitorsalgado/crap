var crypto = require('crypto');

var TOKEN_LENGTH = 32;

module.exports = {
    addDays: function (date, days) {
        var result = new Date(date);
        result.setDate(result.getDate() + days);

        return result;
    }
};
