/* eslint-disable valid-jsdoc */

/**
@fileOverview NetRelations common extensions to jQuery
*/

(function ($) {
	'use strict';

	/**
	Get the element matching the fragment part of an URL via Ajax
	*/
	$.getFragment = function (options) {
		if (typeof options === 'string') {
			options = {
				url: options
			};
		}

		var url = new netr.URI(options.url);

		if (!url.fragment) {
			throw new Error('Url passed to $.getFragment is missing fragment.');
		}

		var fragment = '#' + url.fragment;
		var _success = options.success;

		options.success = function (data, textStatus, jqXHR) {
			data = $($.trim(data));

			var content;

			// Remove any text nodes.
			data = data.filter(function () {
				return this.nodeType !== 3;
			});

			if (data.length) {
				if (data.length === 1) {
					if (data.is(fragment)) {
						content = data;
					}
					else {
						content = $(fragment, data);
					}
				}
				else {
					content = data.find('*').andSelf().filter(fragment);
				}
			}
			else {
				content = $();
			}

			_success(content, textStatus, jqXHR);
		};

		// IE seems to sometimes escape the hash part and send it along to the server.
		// This might cause an error, so we have to take the hash part away before requesting
		url.fragment = '';
		options.url = url.getAbsolute();

		return $.ajax(options);
	};

	/*
	Add selector for external links
	*/
	$.expr[':'].external = function (el) {
		return $(el).isExternal();
	};

	$.extend($.fn, {
		/**
		Returns whether the first link is external
		@memberOf $.fn
		*/
		isExternal: function () {
			var url;
			var doc;

			// Is it a link to begin with?
			if (this.is('a')) {
				url = new netr.URI(this.attr('href'));
				doc = new netr.URI(document.location.toString());

				// A mailto link is not external
				if (url.scheme !== 'mailto:') {
					if (url.getSecondLevelDomain() && doc.getSecondLevelDomain()) {
						// If the second-level domain matches the current one,
						// it's not an external link.
						if (url.getSecondLevelDomain() !== doc.getSecondLevelDomain()) {
							return true;
						}
					}
					else {
						if (url.domain && (url.domain !== doc.domain)) {
							return true;
						}
					}
				}
			}

			return false;
		},

		/**
		Convert obfuscated mailto elements into real links
		*/
		activateEmailLinks: function (options) {
			options = $.extend({}, {
				// Optional element with text to be used as the visible link text
				textSelector: '.email-text:first',
				// Element with obfuscated email adress (entity encoded, decimal or hexadecimal)
				addressSelector: '.email-address:first',
				// Optional prefix to further reduce the risk of spam bots picking up addresses
				salt: 'INGEN_SPAM_'
			}, options || {});

			return this.each(function () {
				var wrapper      = $(this);
				var text_elem    = wrapper.find(options.textSelector);
				var address_elem = wrapper.find(options.addressSelector);

				if (address_elem.length) {
					var link         = $('<a>');
					var link_address = address_elem.text().replace(options.salt, '');
					var link_text    = (text_elem.length ? text_elem.text() : link_address);

					link
						.text(link_text.replace(options.salt, ''))
						.attr('href', 'mailto:' + link_address);

					try {
						// Copy all attributes (except class) from wrapper
						$.each(wrapper.prop('attributes'), function (i, attr) {
							if (typeof wrapper.attr(attr.name) !== 'undefined' && attr.name !== 'class') {
								link.attr(attr.name, attr.value);
							}
						});
					}
					/*eslint-disable */
					catch (e) {}
					/*eslint-enable */

					wrapper.replaceWith(link);
				}
			});
		},

		/**
		Return the height of the tallest matched element
		@memberOf $.fn
		*/
		getHighestHeight: function () {
			var maxHeight = 0;

			// Get the height of the highest element
			this.each(function () {
				var el = $(this);
				var height;

				el.css('min-height', 0);
				height = el.outerHeight();

				if (height > maxHeight) {
					maxHeight = height;
				}
			});

			return maxHeight;
		},

		/**
		Justifies the heights of a bunch of elements to match the highest one
		@memberOf $.fn
		*/
		justify: function () {
			var maxHeight = this.getHighestHeight();

			// Set min-height for all elements
			this.each(function () {
				var el = $(this);

				if (el.css('box-sizing') === 'border-box') {
					el.css('min-height', maxHeight);
				}
				else {
					el.css('min-height', el.height() + maxHeight - el.outerHeight());
				}
			});
			return this;
		},

		/**
		 * Justify a list of items by rows.
		 *
		 * @param {Object} options
		 */
		justifyByRow: function (options) {

			options = $.extend({}, {
				// CSS selector for the item to justify
				itemSelector: '> li',
				// How many items to justify at a time (= items per row).
				items: 3
			}, options || {});

			$(this).each(function () {
				var itemsToJustify = $(this).find(options.itemSelector);
				// Automatically calculate when a new row starts and set heights by row
				if (options.items === 'auto') {
					var currentRowStart = 0;
					var rowElements = [];
					var topPosition = 0;

					itemsToJustify.each(function () {
						var el = $(this);
						topPosition = el.position().top;
						if (currentRowStart !== topPosition) {
							// We just came to a new row.  Set all the heights on the completed row.
							$(rowElements).justify();
							// Set the variables for the new row
							rowElements.length = 0; // empty the array
							currentRowStart = topPosition;
							rowElements.push(el);
						}
						else {
							// Another element on the current row.  Add it to the list.
							rowElements.push(el);
						}
						// Justify the last row
						$(rowElements).justify();
					});
				}
				// Use the supplied number per row
				else {
					for (var i = 0; i < itemsToJustify.length; i = i + options.items) {
						itemsToJustify.slice(i, i + options.items).justify();
					}
				}
			});
		},

		/**
		Center an element horizontally in the viewport
		@memberOf $.fn
		*/
		centerInViewport: function () {
			return this.each(function () {
				var element = $(this);

				element.css({
					left: ($(window).width() / 2) - (element.width() / 2)
				});
			});
		},

		/**
		Get an elements corresponding input
		*/
		getLabel: function (context) {
			context = context || $('body');

			if (this.is('input, select, textarea') && this.attr('id')) {
				return $('label[for=' + this.attr('id') + ']', context);
			}
		},

		/**
		Generate a random id for the element(s)
		@param {Boolean} [overwrite] Whether to overwrite an exisiting id
		*/
		generateRandomId: function (overwrite) {
			overwrite = overwrite === false ? false : true;
			return this.each(function () {
				var el = $(this);
				var id;
				if (overwrite || !el.attr('id')) {
					// Loop just to be sure the id doesn't already exist in the DOM
					do {
						id = Math.random().toString().replace(/\D/, '');
					} while ($('#' + id).length);
					el.attr('id', id);
				}
			});
		}
	});
}(jQuery));
/* eslint-enable valid-jsdoc */
