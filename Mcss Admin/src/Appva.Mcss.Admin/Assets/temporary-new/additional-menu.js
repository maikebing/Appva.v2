/* globals netr */
(function ($) {
	'use strict';

	// Open in dialog on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.additional-menu', context).additionalMenu();
	});

	$.fn.additionalMenu = function () {
		this.each(function () {
			var self = $(this);
			var selfPosition = self.position();
			var menuElement = self.find('.additional-menu__list');
			var oldHeader = self.find('.additional-menu__trigger');

			var trigger = $('<button/>', {
				'aria-expanded': 'false',
				class: 'additional-menu__trigger button button--icon-only'
			}).append(oldHeader.contents()).on('click', function () {
				if (trigger.attr('aria-expanded') === 'false') {
					trigger.attr('aria-expanded', 'true');
					self.addClass('additional-menu--open');
				}
				else {
					trigger.attr('aria-expanded', 'false');
					self.removeClass('additional-menu--open');
				}
			});

			// replace old header with new button
			oldHeader.replaceWith(trigger);
			// Remove tabindex
			self.removeAttr('tabindex');

			// Run the menu alignment a first time
			menuAlignment();
			$(window).resize(netr.throttle(function () {
				menuAlignment();
			}, 500));

			// Adds-class to the menu if it's on the right side of the browser window
			function menuAlignment() {
				selfPosition = self.position();
				if (($(window).width() / 2) < selfPosition.left) {
					menuElement.addClass('additional-menu__list--right');
				}
				else {
					menuElement.removeClass('.additional-menu__list--right');
				}
			}
		});
	};

}(jQuery));