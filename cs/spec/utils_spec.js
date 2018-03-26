"use strict";

const textUtils = require("../app/lib/utils/textUtils");

describe("Utils", () => {
	it("when invalid email, isEmail should return false", () => {
		const email = "test";

		expect(textUtils.isEmail(email)).toBe(false);
	});

	it("when valid email, isEmail should return true", () => {
		const email = "test@uol.com.br";

		expect(textUtils.isEmail(email)).toBe(true);
	});
});
