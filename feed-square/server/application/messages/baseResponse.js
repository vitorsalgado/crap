var BaseResponse = function() {
	this.success = true;
	this.message = '';
};

BaseResponse.createErrorResponse = function(message) {
	var response = new BaseResponse();

	response.message = message;
	response.success = false;

	return response;
};

module.exports = BaseResponse;
