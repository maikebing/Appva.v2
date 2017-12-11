/**
 * Post single input
 * @requires update-section.js
 */
(function ($) {
	'use strict';

	// init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('[data-post-single-input-length]', context).postSingleInput();
	});

	$.fn.postSingleInput = function () {
		this.each(function () {
			var self = $(this);
			var form = self.closest('form');

			// At what length should post be triggered_
			var triggerLength = self.data('post-single-input-length');

			self.on('keyup paste', function () {
				var url;
				if (self.val().length === triggerLength) {
					if (self.valid()) {
						var data = self.serialize();
						var target = $('#' + self.data('update-section-target'));
						if (target.length) {
							self.attr('aria-controls', target);
							// TODO: Be able to insert other URL:s
							if (form.attr('data-update-section')) {
								url = form.attr('data-update-section');
							}
							else {
								url = form.attr('action');
							}

							$.updateTarget({
								target: target,
								url: url,
								data: data
							});
						}
						else {
							// this way the function can be used to post the whole
							// form using update section for example.
							form.submit();
						}
					}
				}
			});
		});
	};
}(jQuery));