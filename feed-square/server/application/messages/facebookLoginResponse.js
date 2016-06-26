var util = require('util');
var BaseResponse = require('./baseResponse');

var FacebookLoginResponse = function(accountDto, accessToken) {
	BaseResponse.call(this);
	this.account = accountDto;
    this.token = accessToken;
};

util.inherits(BaseResponse, FacebookLoginResponse);

module.exports = FacebookLoginResponse;
