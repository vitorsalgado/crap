"use strict";

const express = require("express");
const bodyParser = require("body-parser");
const mongoose = require("mongoose");
const port = process.env.PORT || 3000;
const routes = require("./app/routes");
const swagger = require("./swagger/swagger");

let app = express();

require("dotenv").config();
swagger(app);

mongoose.connect(process.env.MONGODB_URI);
mongoose.Promise = global.Promise;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));

app.disable("x-powered-by");
app.set("etag", false);

app.use(routes(app));

app.listen(port, () => {
	console.log(`CS Desafio Node is online on port ${port}`);
	console.log("Navigate to / to see API documentation");
});
