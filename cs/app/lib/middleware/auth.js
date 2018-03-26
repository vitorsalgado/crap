"use strict";

const Unauthorized = require("../httpErrors").Unauthorized;

module.exports = (req, res, next) => {
	if (!req.headers || !req.headers.authorization && req.headers.authorization.split(" ") != 2) {
		return next(new Unauthorized());
	}

	const bearer = req.headers.authorization.split(" ");
	const type = bearer[0];
	const token = bearer[1];

	if (!(/^Bearer$/i.test(type) || !token)) {
		return new next(new Unauthorized("Não autorizado."));
	}

	req.token = token;

	return next();
};
