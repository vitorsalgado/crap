"use strict";

const User = require("../models/user");
const encrypt = require("../lib/encrypt");

module.exports.isEmailRegistered = (email) => new Promise((resolve, reject) => {
	User.findOne({email: email}, (err, doc) => {
		if (doc || err) {
			return reject(err);
		}

		return resolve();
	});
});

module.exports.findByEmail = (email) => new Promise((resolve, reject) => {
	User.findOne({email: email}, (err, doc) => {
		if (err) {
			return reject(err);
		}

		return resolve(doc);
	});
});

module.exports.validateCredentials = (user, incoming_password) => {
	const hash = encrypt.createPasswordHash(incoming_password, user.salt);

	return hash === user.senha;
};

module.exports.signIn = (user) => new Promise((resolve, reject) => {
	user.ultimo_login = Date.now();
	user.data_atualizacao = Date.now();
	user.token = encrypt.sign(user._id);

	user.save((err, doc) => {
		if (err) {
			return reject(err);
		}

		return resolve(doc);
	});
});

module.exports.findById = (id) => new Promise((resolve, reject) => {
	User.findOne({_id: id}, (err, doc) => {
		if (err) {
			return reject(err);
		}

		return resolve(doc);
	});
});

module.exports.save = (model) => new Promise((resolve, reject) => {
	const salt = encrypt.createSalt();
	const hashedPwd = encrypt.createPasswordHash(model.senha, salt);

	let user = new User({
		nome: model.nome,
		email: model.email,
		senha: hashedPwd,
		salt: salt,
		telefones: model.telefones,
		data_atualizacao: Date.now(),
		ultimo_login: Date.now()
	});

	user.token = encrypt.sign(user._id);

	user.save((err, doc) => {
		if (err) {
			return reject(err);
		}

		return resolve(doc);
	});
});
