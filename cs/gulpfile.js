"use strict";

const gulp = require("gulp");
const eslint = require("gulp-eslint");

gulp.task("lint", () =>
	gulp.src(["**/*.js", "!node_modules/**", "!spec"])
		.pipe(eslint())
		.pipe(eslint.format())
		.pipe(eslint.failAfterError())
);

gulp.task("default", ["lint"], function () {

});
