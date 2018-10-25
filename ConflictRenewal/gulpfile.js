var gulp = require('gulp'),
    util = require("gulp-util"),
    sass = require("gulp-sass"),
    autoprefixer = require('gulp-autoprefixer'),
    rename = require('gulp-rename'),
    log = util.log;
minifycss = require('gulp-clean-css');

gulp.task("sass", function () {
    log("Generate CSS files " + (new Date()).toString());
    gulp.src(["wwwroot/lib/bootstrap/scss/**/*.scss", "wwwroot/lib/bootstrap/scss/mixins/**/*.scss", "wwwroot/lib/bootstrap/scss/utilities/**/*.scss"])
        .pipe(sass({ style: 'expanded' }))
        .pipe(autoprefixer("last 3 version", "safari 5", "ie 8", "ie 9"))
        .pipe(gulp.dest("wwwroot/css/bootstrap"))
        .pipe(rename({ suffix: '.min' }))
        .pipe(minifycss())
        .pipe(gulp.dest('wwwroot/css/bootstrap'));
});

gulp.task("watch", function () {
    log("Watching scss files for modifications");
    gulp.watch(["wwwroot/lib/bootstrap/scss/**/*.scss", "wwwroot/lib/bootstrap/scss/mixins/**/*.scss", "wwwroot/lib/bootstrap/scss/utilities/**/*.scss"], ["sass"]);
});