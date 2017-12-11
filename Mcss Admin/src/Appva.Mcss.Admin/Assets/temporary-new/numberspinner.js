(function ($) {
	'use strict';

	// init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('[type="number"]', context).numberSpinner();
	});

	$.fn.numberSpinner = function (settings) {
		var config = {
		};
		if (settings) {
			$.extend(config, settings);
		}

		this.each(function () {

			var element = $(this);
			var label = $('[for="' + element.attr('id') + '"]').html();
			var decrease = $('<button class="button decrease" tabindex="-1"><span class="visually-hidden">' + netr.string.translate('spinner.decrease') + ' ' + label + '</span><svg class="icon icon--small icon--s" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#arrow--fat-icon" /></svg></button>');
			var increase = $('<button class="button increase" tabindex="-1"><span class="visually-hidden">' + netr.string.translate('spinner.increase') + ' ' + label + '</span><svg class="icon icon--small" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#arrow--fat-icon" /></svg></button>');
			var wrapper = $('<span class="numberspinner">');
			var max = parseFloat(element.attr('max'));
			var min = parseFloat(element.attr('min'));
			if (element.data('direction') !== 'vertical') {
				decrease.find('svg').replaceWith($('<svg class="icon icon--small" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#minus-icon" /></svg>'));
				increase.find('svg').replaceWith($('<svg class="icon icon--small" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#plus-icon" /></svg>'));
			}
			else {
				wrapper.addClass('numberspinner--vertical');
			}
			decrease.add(increase).find('svg').on('click', function () {
				$(this).parent().trigger('click');
			});

			if (isNaN(min)) {
				min = -Infinity;
			}
			if (isNaN(max)) {
				max = Infinity;
			}
			var step = parseFloat(element.attr('step')) || 1;

			// Click on increase and decrease
			decrease.add(increase).on('click', function (event) {
				event.preventDefault();

				var target = event.target;
				var inputValue;

				inputValue = parseFloat(element.val(), 10);

				// is input empty or smallar than min attribute?
				if (isNaN(inputValue) || inputValue < min) {
					// Sets the value to input's min-attribute, unless negative, then it becomes 0.
					if (min < 0 && max > 0) {
						element.val(0);
					}
					else if (min >= 0) {
						element.val(min);
					}
					else if (max < 0) {
						element.val(max);
					}
				}

				// If value is larger than the max attribute
				if (inputValue > max) {
					element.val(max);
				}

				// if min is not set, set base-value to zero (-Infinity is no good value). Just
				// to be able to calculate modulus/remainders
				var baseValue = min;
				if (min === -Infinity) {
					baseValue = 0;
				}

				// When pressing (-), decrease value with step, unless it would become less than min
				// note that we check if value becomes greater than max as well. In case of the user had
				// entered a too large number with the keyboard
				if ($(target).hasClass('decrease') && (inputValue - step) >= min && inputValue - step <= max) {
					// makes the value always divisible by step

					inputValue = inputValue - ((inputValue - (baseValue % step)) % step);
					element.val(inputValue - step);
					element.trigger('change');
				}
				// When pressing (+), increase value with step, unless it would become more than max
				// note that we check if value becomes smaller than min as well. In case of the user had
				// entered a too small number with the keyboard
				else if ($(target).hasClass('increase') && inputValue + step <= max && (inputValue + step) >= min) {
					// makes the value always divisible by step
					// not that % is not a true modulus, so the result of the operation might be negative
					inputValue = inputValue - ((inputValue - (baseValue % step)) % step);
					element.val(inputValue + step);
					element.trigger('change');
				}

				// validate element if possible, to get rid of any error messages.
				if (typeof $(element).valid === 'function') {
					element.valid();
				}

				element.focus();
			});

			wrapper.on('selectstart', function (event) {
				// Target check because otherwise text cannot be selected in Internet Explorer
				if (!$(event.target).is('input')) {
					event.preventDefault();
				}
			});

			element.wrap(wrapper);
			element.before(decrease).after(increase);
		});
	};

}(jQuery));