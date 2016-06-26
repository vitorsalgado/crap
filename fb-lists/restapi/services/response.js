var Response = function(data) {
	this.success = true;
	this.message = '';
    this.data = data;
};

Response.createError = function(message) {
	var response = new Response();

	response.message = message;
	response.success = false;

	return response;
};

module.exports = Response;