(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('select', context).customSelect();
	});

	// Custom select!

	$.fn.customSelect = function (settings) {
		var config = {
			replacedClass: 'replaced', // Class name added to replaced selects
			customSelectClass: 'custom-select__select', // Class name of the (outer) inserted span element
			activeClass: 'custom-select__select--is-active', // Class name assigned to the fake select when the real select is in hover/focus state
			disabledClass: 'custom-select__select--is-disabled', // Class name assigned to the fake select when the real select is in hover/focus state
			wrapperElement: '<div class="custom-select" />', // Element that wraps the select to enable positioning
			icon: '<span class="custom-select__icon"><svg class="icon icon--s" focusable="false" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#arrow--fat-icon" /></svg></span>'
		};
		if (settings) {
			$.extend(config, settings);
		}
		this.each(function () {
			var select = $(this);
			select.addClass(config.replacedClass);
			select.wrap(config.wrapperElement);

			var update = function (customSelect) {
				var option = select.find('option:selected');
				var val = option.text();

				// remove disabled class (we won't need to put this back because a disabled option cannot be selected once
				// it's deselected)
				customSelect.removeClass(config.disabledClass);

				// Change the text on the faux select
				customSelect.find('.custom-select__text').text(val);
			};

			/* Create and insert the spans that will be styled as the fake select
			 * To prevent screen readers from announcing the fake select in addition to the real one,
			 * aria-hidden is used to hide it.
			 */
			var customSelect = $('<span class="' + config.customSelectClass + '" aria-hidden="true"><span class="custom-select__text">' + $('option:selected', this).text() + '</span>' + config.icon + '</span>');

			// Adds class to enable styling on disabled option
			if ($('option:selected', this).prop('disabled')) {
				customSelect.addClass(config.disabledClass);
			}

			customSelect.insertAfter(select);
			if ($('option:selected').prop('disabled') === true) {
				$('.custom-select__text').addClass('custom-select__placeholder');
			}

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