(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('select.select-style', context).customSelect();
	});

	// Custom select!

	$.fn.customSelect = function (settings) {
		var config = {
			replacedClass: 'replaced', // Class name added to replaced selects
			customSelectClass: 'custom-select__select', // Class name of the (outer) inserted span element
			activeClass: 'is-active', // Class name assigned to the fake select when the real select is in hover/focus state
			wrapperElement: '<div class="custom-select" />', // Element that wraps the select to enable positioning
			icon: '<span class="custom-select__icon"><svg class="icon" aria-hidden="true"><use xlink:href="/Assets/images/icons/icon.sprite.svg#angle-arrow-down-icon" /></svg></span>'
		};
		if (settings) {
			$.extend(config, settings);
		}
		this.each(function () {
			var select = $(this);
			select.addClass(config.replacedClass);
			select.wrap(config.wrapperElement);

			var update = function (customSelect) {
				var val = select.find('option:selected').text();
				customSelect.find('.custom-select__text').text(val);
			};

			/* Create and insert the spans that will be styled as the fake select
			 * To prevent screen readers from announcing the fake select in addition to the real one,
			 * aria-hidden is used to hide it.
			 */
			var customSelect = $('<span class="' + config.customSelectClass + '" aria-hidden="true"><span class="custom-select__text">' + $('option:selected', this).text() + '</span>' + config.icon + '</span>');

			customSelect.insertAfter(select);

			// Add or remove a class name to enable styling of hover/focus states
			select.on({
				mouseenter: function () {
					customSelect.addClass(config.activeClass);
				},
				mouseleave: function () {
					customSelect.removeClass(config.activeClass);
				},
				focus: function () {
					customSelect.addClass(config.activeClass);
				},
				blur: function () {
					customSelect.removeClass(config.activeClass);
				},
				// Update the fake select when the real select’s value changes
				change: function () {
					update(customSelect);
				},
				/* Gecko browsers don't trigger onchange until the select closes, so
				 * changes made by using the arrow keys aren't reflected in the fake select.
				 * See https://bugzilla.mozilla.org/show_bug.cgi?id=126379.
				 * IE normally triggers onchange when you use the arrow keys to change the selected
				 * option of a closed select menu. Unfortunately jQuery doesn’t seem able to bind to this.
				 * We could overwrite any existing onchange handlers, but that isn’t nice.
				 * As a workaround the text is also updated when any key is pressed and then released.
				 */
				keyup: function () {
					update(customSelect);
				}
			});
		});
	};
}(jQuery));