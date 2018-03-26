"use strict";

const userCtrl = require("../app/controllers/userCtrl");
const BadRequest = require("../app/lib/httpErrors").BadRequest;

describe(("User"), () => {
	it("user should be registered", () => {
		const req = {body: {nome: "vitor", email: "vitor@email.com", senha: "123"}};
		const res = {json: () => {}, status: 201};
		const stub = {next: () => {}};

		spyOn(stub, "next");

		userCtrl.inject({
			isEmailRegistered: () => Promise.resolve(),
			save: () => Promise.resolve(req.body)
		});

		userCtrl.register(req, res, stub.next);

		expect(res.status).toBe(201);
		expect(stub.next).not.toHaveBeenCalled();
	});

	it("should not accept user with invalid email", () => {
		const req = {body: {nome: "vitor", email: "invalid", senha: "123"}};
		const res = {json: () => {}, status: 400};
		const stub = {next: () => {}};

		spyOn(stub, "next");

		userCtrl.inject({
			isEmailRegistered: () => Promise.resolve(),
			save: () => Promise.resolve(req.body)
		});

		userCtrl.register(req, res, stub.next);

		expect(res.status).toBe(400);
		expect(stub.next).toHaveBeenCalledWith(jasmine.any(BadRequest));
	});
});
