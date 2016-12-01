"use strict";

class BadRequest extends Error {
	constructor(message = "Bad Request") {
		super(message);
		this.status = 400;
	}
}

class Unauthorized extends Error {
	constructor(message = "Unauthorized") {
		super(message);
		this.status = 401;
	}
}

class NotFound extends Error {
	constructor(message = "Not Found") {
		super(message);
		this.status = 404;
	}
}

module.exports.BadRequest = BadRequest;
module.exports.Unauthorized = Unauthorized;
module.exports.NotFound = NotFound;
