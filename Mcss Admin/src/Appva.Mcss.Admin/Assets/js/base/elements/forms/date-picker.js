(function ($) {
	'use strict';
	// init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.datepicker', context).each(function () {
			var datepickerInput = $(this);

			// Defaults
			var beforeShowDay = null;
			var onSelect = null;
			var onClose = null;
			var minDate = null;
			var maxDate = null;

			// TODO: make this an ordinary if-chain?
			switch (datepickerInput.data('datepicker-limit')) {
				case 'future':
					minDate = 0;
					break;
				case 'past':
					maxDate = 0;
					break;
				default:
					// your vanilla datepicker
					break;
			}

			// Are there SELECTED dates
			if (typeof datepickerInput.data('datepicker-selected-dates') !== 'undefined') {
				// With an array of dates to select eg:
				// data-datepicker-selected-dates="<?= htmlspecialchars('["2017-04-24","2017-04-25","2017-04-26"]') ?>"
				// the php is needed to make html-entities from the special chars.
				beforeShowDay = function (date) {
					var dates = jQuery.datepicker.formatDate('yy-mm-dd', date);
					var excludedDates = datepickerInput.data('datepicker-selected-dates') || [];
					if (excludedDates.indexOf(dates) === -1) {
						return [true];
					}
					else {
						// add selected class
						return [true, 'ui-datepicker__selected'];
					}
				};

				onSelect = function () {

					// this event passes the value of the input to another function.
					// For example the input-value-list
					$(this).trigger('softpost');

					// Prevents the datepicker from closing.
					$(this).data('datepicker').inline = true;
				};
				onClose = function () {
					$(this).data('datepicker').inline = false;
				};
			}

			// Are there DISABLED dates
			// TODO: Make it possible to combine selected and disabled dates
			if (typeof datepickerInput.data('datepicker-disabled-dates') !== 'undefined') {
				// With an array of dates to exclude eg:
				// data-datepicker-disabled-dates="<?= htmlspecialchars('["2017-04-24","2017-04-25","2017-04-26"]') ?>"
				// the php is needed to make html-entities from the special chars.
				beforeShowDay = function (date) {
					var dates = jQuery.datepicker.formatDate('yy-mm-dd', date);
					var excludedDates = datepickerInput.data('datepicker-disabled-dates') || [];
					if (excludedDates.indexOf(dates) === -1) {
						return [true];
					}
					else {
						// add disabled class
						return [false, 'ui-datepicker__disabled'];
					}
				};
			}

			// call the datepicker!
			// TODO: Date format by locale
			datepickerInput.datepicker({
				showOn: 'both',
				dateFormat: 'yy-mm-dd',
				regional: 'sv',
				minDate: minDate,
				maxDate: maxDate,
				showWeek: true,
				numberOfMonths: 12,
				onSelect: onSelect,
				onClose: onClose,
				buttonImage: '/project/images/icons/calendar.svg',
				buttonImageOnly: true,
				buttonText: netr.string.translate('datepicker.selectDate'),
				beforeShowDay: beforeShowDay
			});
		});

		// remove validation errors upon selecting date from datepicker
		$('.datepicker').on('change', function () {
			var self = $(this);
			if (typeof self.valid === 'function') {
				self.valid();
			}
		});
	});
}(jQuery));