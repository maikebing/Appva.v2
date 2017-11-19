/**
 * Mouse click class toggler
 */
(function ($) {
	'use strict';

	$('body')
	.on('mousedown', function () {
		$('html').addClass('mousedown');
	})
	.on('keydown', function () {
		$('html').removeClass('mousedown');
	});

}(jQuery));