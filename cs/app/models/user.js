"use strict";

const mongoose = require("mongoose");
const uuid = require("uuid");

const User = new mongoose.Schema({
	_id: {type: String, default: () => uuid.v1()},
	nome: {type: String, trim: true},
	email: {type: String, lowercase: true, index: {unique: true}},
	salt: String,
	senha: String,
	telefones: {type: [{numero: Number, ddd: Number}], required: false},
	data_criacao: {type: Date, default: Date.now},
	data_atualizacao: Date,
	ultimo_login: Date,
	token: String
});

module.exports = mongoose.model("User", User);
