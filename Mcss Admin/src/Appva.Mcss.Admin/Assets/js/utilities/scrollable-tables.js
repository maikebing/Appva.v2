(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('table', context).scrollabletables();
	});

	// Scrollable tables!
	var tables = $();

	$.fn.scrollabletables = function () {
		tables = tables.add(this);
		return this;
	};

	$(window).bind('load resize orientationchange', function () {
		// Check if any tables are wider than their parent.
		// If they are, wrap them in containers to allow for horizontal scrolling.
		tables.each(function () {
			var table = $(this);
			if (table.outerWidth() > table.parent().outerWidth()) {
				if (!table.hasClass('scrollable')) {
					table
						.wrap('<div class="scrollable-table"><div class="inner"></div></div>')
						.addClass('scrollable');

					table.parent().on('scroll', netr.throttle(function () {
						var self = $(this);
						var shadow_div = self.parent();

						if (self.scrollLeft() + 1 >= self.find('table').outerWidth() - self.outerWidth()) {
							shadow_div.addClass('no-shadow-right');
						}
						else {
							shadow_div.removeClass('no-shadow-right');
						}

						if (self.scrollLeft() > 0) {
							shadow_div.addClass('shadow-left');
						}
						else {
							shadow_div.removeClass('shadow-left');
						}
					}, 100));
				}
			}
			else if (table.hasClass('scrollable')) {
				table.removeClass('scrollable').unwrap().unwrap();
			}
		});
	});
}(jQuery));