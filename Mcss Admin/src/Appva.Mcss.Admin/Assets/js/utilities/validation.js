(function ($) {
	'use strict';

	// Add methods
	// Personnummer
	$.validator.addMethod('personnumber', function (value) {
		return /^(([A-Z]|[0-9])(-*|\s)*){12}$/i.test(value);
	});

	// Input greater than other input
	// It even works on dates!
	// Right now it actually validates on greater _or equal_
	jQuery.validator.addMethod('greaterThan', function (value, element, params) {
		if ($(params).val() !== '' && value !== '') {
			if (!/Invalid|NaN/.test(new Date(value))) {

				var firstDate = new Date($(params).val());
				var secondDate = new Date(value);

				// 86400000 = 24 hours in milliseconds. In other words: we're counting days.
				var daysBetween = Math.floor((secondDate.getTime() - firstDate.getTime()) / 86400000);

				return daysBetween >= 0;
			}
			return isNaN(value) && isNaN($(params[0]).val()) || (Number(value) >= Number($(params[0]).val()));
		}
		return true;
	}, 'Must be greater than {1}.');

	// Is the date restricted?
	// Works best with datepicker.
	jQuery.validator.addMethod('restrictedDate', function (value, element) {
		// value: The value of the input being validated
		// element: the element being validated
		// Returns true if valid
		// Message, not quite sure what replaces {1}....

		if (value !== '') {
			var excludedDates = $(element).data('datepicker-disabled-dates') || [];
			if (excludedDates.indexOf(value) === -1) {
				return true;
			}
			else {
				return false;
			}
		}
		return true;
	}, 'Date is not available for selection');

	// Is the date in the past?
	// Works best with datepicker.
	jQuery.validator.addMethod('datelimit', function (value, element, param) {
		// value: The value of the input being validated
		// element: the element being validated
		// Returns true if valid

		if (value !== '') {
			var date = new Date(value);
			var today = new Date();
			// 86400000 = 24 hours in milliseconds
			var daysBetween = Math.ceil((date.getTime() - today.getTime()) / 86400000);
			if (param === 'past') {
				return (daysBetween <= 0);
			}
			else {
				return (daysBetween >= 0);
			}
		}
		return true;
	}, function (argument) {
		// This a little odd having a function as a message, but it makes it possible to
		// have two different messages
		return {
			'future': 'Select a future date (or today)',
			'past': 'Select a past date (or today)'
		}[argument];
	});

	// Messages
	jQuery.extend(jQuery.validator.messages, {
		personnumber: 'Enter a valid Swedish social security number'
	});

	// A correct step validator (overriding the built in one)
	$.validator.methods.step = function (value, element, param) {
		var type = $(element).attr('type');
		var errorMessage = 'Step attribute on input type ' + type + ' is not supported.';
		var supportedTypes = ['text', 'number', 'range'];
		var re = new RegExp('\\b' + type + '\\b');
		var notSupported = type && !re.test(supportedTypes.join());
		var min = parseFloat($(element).attr('min'));

		// If no min value is set, it becomes 0 to be able to calculate modulus/remainders
		if (isNaN(min)) {
			min = 0;
		}

		// Works only for text, number and range input types
		// TODO: find a way to support input types date, datetime, datetime-local, month, time and week
		if (notSupported) {
			throw new Error(errorMessage);
		}

		// Fixed: Start steps from min value instead of zero
		return this.optional(element) || ((value - min) % param === 0);
	};

	$('body').on('newcontent', function (e) {
		var context = e.target;

		// Initiate validation
		// 1. can be saved as variable to use its methods
		// ----------------------------------------------------------------------
		$('.validate', context).each(function () {
			$(this).validate({ /* [1] */
				// Replaces the extra label input with a em-element
				errorElement: 'div',
				errorClass: 'control-container__error-message',
				errorPlacement: function (error, element) {
					var controlContainer = element.closest('.control-container');
					// If the input is in a control container. The error should be appended to that.
					// This is to make number-spinners work with errors (otherwise the error would've ended
					// up before the increase button)
					if (controlContainer.length > 0) {
						error.appendTo(element.closest('.control-container'));
					}
					else {
						error.insertAfter(element);
					}
				},
				highlight: function (element, errorClass, validClass) {
					$(element).parents('div.control-group')
						.addClass(errorClass)
						.removeClass(validClass);
				},
				unhighlight: function (element, errorClass, validClass) {
					$(element).parents('.error')
						.removeClass(errorClass)
						.addClass(validClass);
				}
			});
		});
	});
}(jQuery));