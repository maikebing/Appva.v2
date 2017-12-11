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

			// Remove type="date" attribute to prevent browsers from going bonkers with their own styling and datepickers
			datepickerInput.attr('type', 'text');

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

			// Should we check the date against a database query?
			if (datepickerInput.data('datepicker-dbquery') === true) {
				var self = $(this);

				// On input, check that this date is permitted
				self.on('change', function () {

					$.ajax({
						url: 'check',
						type: 'GET',
						data: {'Date': this.value},
						success: function (data) {
							var result = JSON.parse(data);
							if (result.Content === 'fail') {
								var warningElement = $('<div class="form-controls__error-message">' + result.Message + '</div>');

								// Disable the submit button
								self.closest('form').find('[type="submit"]').prop('disabled', true);

								self.parent().append(warningElement);

								// Add listener to remove the warning if a new date is selected
								self.change(function () {
									warningElement.remove();
									self.closest('form').find('[type="submit"]').prop('disabled', false);
								});
							}
							else {
								if (self.attr('datepicker-fill-end') === 'true') {
									fillEndDate(self);
								}
							}
						},
						statusCode: {
							404: function () {
								console.log('404 error');
							}
						}
					});
				});
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

		function fillEndDate(self) {
			var inputs = self.closest('form').find('.date-picker input');
			var endDate = $(inputs[inputs.index(self) + 1]);

			endDate.val(self.val());
		}

		// remove validation errors upon selecting date from datepicker
		$('.datepicker').on('change', function () {
			var self = $(this);
			if (typeof self.valid === 'function') {
				self.valid();

				if ((self.attr('datepicker-fill-end') === 'true') && (self.data('datepicker-dbquery') !== true)) {
					fillEndDate(self);
				}
			}
		});
	});
}(jQuery));