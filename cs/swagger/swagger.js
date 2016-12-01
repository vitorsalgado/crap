"use strict";

const swaggerTools = require("swagger-tools");
const jsyaml = require("js-yaml");
const fs = require("fs");

let options = {
	swaggerUi: "/swagger.json",
	controllers: "./app/controllers",
	useStubs: process.env.NODE_ENV === "development"
};

let spec = fs.readFileSync("./swagger/swagger.yaml", "utf8");
let swaggerDoc = jsyaml.safeLoad(spec);

let Swagger = (app) => {
	swaggerTools.initializeMiddleware(swaggerDoc, (middleware) => {
		app.use(middleware.swaggerMetadata());
		app.use(middleware.swaggerValidator());
		app.use(middleware.swaggerRouter(options));
		app.use(middleware.swaggerUi());
	});
};

module.exports = Swagger;
