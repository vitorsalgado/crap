"use strict";

const BadRequest = require("../lib/httpErrors").BadRequest;
const Unauthorized = require("../lib/httpErrors").Unauthorized;
const NotFound = require("../lib/httpErrors").NotFound;
const textUtils = require("../lib/utils/textUtils");

let userService = require("../services/userService");
let encrypt = require("../lib/encrypt");

// allows change dependencies in unit tests
module.exports.inject = (userSvc) =>
	userService = userSvc;

module.exports.register = (req, res, next) => {
	const email = req.body.email;
	const nome = req.body.nome;
	const senha = req.body.senha;

	if (!email || email.trim() === "") {
		return next(new BadRequest("E-mail é obrigatório."));
	}

	if (!textUtils.isEmail(email)) {
		return next(new BadRequest("Formato de e-mail inválido."));
	}

	if (!nome || nome.trim() === "") {
		return next(new BadRequest("Nome é obrigatório."));
	}

	if (!senha || senha.trim() === "") {
		return next(new BadRequest("Senha é obrigatória."));
	}

	userService.isEmailRegistered(req.body.email)
		.then(() =>
			userService.save({nome: nome, email: email, senha: senha, telefones: req.body.telefones})
				.then((user) => res.status(201).json(toDto(user)))
				.catch((err) => next(err))
		)
		.catch(() => next(new BadRequest("E-mail já existente.")));
};

module.exports.login = (req, res, next) => {
	const email = req.body.email;
	const senha = req.body.senha;
	const default_unauthorized_msg = "Usuário e/ou senha inválidos";

	if (!email || email.trim() === "") {
		return next(new BadRequest("E-mail é obrigatório."));
	}

	if (!textUtils.isEmail(email)) {
		return next(new BadRequest("Formato de e-mail inválido."));
	}

	if (!senha || senha.trim() === "") {
		return next(new BadRequest("Senha é obrigatória."));
	}

	userService.findByEmail(email.toLowerCase())
		.then((user) => {
			if (userService.validateCredentials(user, senha)) {
				return userService.signIn(user)
					.then((loggedUser) => res.status(200).json(toDto(loggedUser)))
					.catch(() => next(new Unauthorized(default_unauthorized_msg)));
			}

			return next(new Unauthorized(default_unauthorized_msg));
		})
		.catch(() => next(new Unauthorized(default_unauthorized_msg)));
};

module.exports.get = (req, res, next) => {
	if (!req.token) {
		return next(new Unauthorized());
	}

	if (!req.params || !req.params.id || req.params.id.trim() === "") {
		return next(new BadRequest("Id é obrigatório"));
	}

	userService.findById(req.params.id)
		.then((user) => {
			if (user.token !== req.token) {
				return next(new Unauthorized("Não autorizado"));
			}

			return encrypt.validateToken(req.token)
				.then(() => res.status(200).json(toDto(user)))
				.catch(() => next(new Unauthorized("Sessão inválida")));
		})
		.catch(() => next(new NotFound(`Não foi encontrado um usuário com o ID ${req.params.id}`)));
};

let toDto = (user) => {
	let tel_func = (tel) => {
		return {id: tel._id, numero: tel.numero, ddd: tel.ddd};
	};

	return {
		id: user._id,
		nome: user.nome,
		email: user.email,
		telefones: user.telefones.map(tel_func),
		data_criacao: user.data_criacao,
		data_atualizacao: user.data_atualizacao,
		ultimo_login: user.ultimo_login,
		token: user.token
	};
};
