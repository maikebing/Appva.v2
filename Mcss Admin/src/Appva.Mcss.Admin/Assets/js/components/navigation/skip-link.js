// Make skip links work in more browsers
(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.skip-link', context).skipLink();
	});

	// Skip link!
	$.fn.skipLink = function () {
		this.each(function () {
			$(this).on('click', 'a', function () {
				var link = $(this);
				var target = $(link.attr('href'));
				if ((target.attr('tabindex') === undefined) || (target.attr('tabindex') === '')) {
					target.attr('tabindex', '-1');
				}
				target.focus();
			});
		});
	};

}(jQuery));