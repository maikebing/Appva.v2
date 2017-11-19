(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('[data-show-message-if-empty]', context).showMessageIfEmpty();
	});

	// toggle by input!
	$.fn.showMessageIfEmpty = function (settings) {
		var config = {
			messageDataAttribute: 'show-message-if-empty',
			classDataAttribute: 'show-message-if-empty-class'
		};

		if (settings) {
			$.extend(config, settings);
		}

		this.each(function () {
			var container = $(this);
			var messageText = container.data(config.messageDataAttribute);
			var messageClass = container.data(config.classDataAttribute) ? container.data(config.classDataAttribute) : 'system-message';
			var message = $('<div class="' + messageClass + '"><p>' + messageText + '</p></div>');

			// The message is added to the beginning of the container.
			container.prepend(message);

			// This is a custom event, triggered by scripts that manipulate content in
			// the container (so far only "toggle by input")
			container.trigger('visibilitychange');

			container.on('visibilitychange', function () {
				if ($(this).children().not(message).filter(':visible').length) {
					message.hide();
				}
				else {
					message.show();
				}
			});
		});
	};

}(jQuery));