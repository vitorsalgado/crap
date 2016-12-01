"use strict";

const encrypt = require("../app/lib/encrypt");

describe("Security", () => {
	it("should return valid salt", () => {
		let salt = encrypt.createSalt();

		expect(salt).toBeDefined();
	});

	it("should return hashed password", () => {
		let salt = encrypt.createSalt();
		let password = "123";

		let hash = encrypt.createPasswordHash(password, salt);

		expect(hash).toBeDefined();
		expect(hash).toEqual(encrypt.createPasswordHash(password, salt));
	});
});