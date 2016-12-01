"use strict";

const express = require("express");
const router = express.Router();
const NotFound = require("./lib/httpErrors").NotFound;
const healthCtrl = require("./controllers/healthCtrl");
const userCtrl = require("./controllers/userCtrl");
const authMdlw = require("./lib/middleware/auth");

let Routing = (app) => {
	router.get("/api/check", healthCtrl.serverCheck);

	router.post("/api/user", userCtrl.register);
	router.put("/api/user", userCtrl.login);
	router.get("/api/user/:id", authMdlw, userCtrl.get);

	// send 404 for all non handled routes
	router.all("*", (req, res, next) => {
		let path = req.path.toLowerCase();

		// avoid send 404 for swagger routes
		if (path.startsWith("/docs") || path.startsWith("/api-docs")) {
			return next();
		}

		return next(new NotFound());
	});

	// general error handler
	router.use((err, req, res, next) => {
		if (res.headersSent) {
			return next(err);
		}

		if (!err) {
			return next();
		}

		console.log(err);

		// returns a default json for all errors on api
		res.status(err.status || 500)
			.json({mensagem: err.message || "Internal Server Error"})
			.end();
	});

	app.use(router);

	return router;
};

module.exports = Routing;
