(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.table-general', context).tableGeneral();
	});

	// Table general
	$.fn.tableGeneral = function (settings) {
		var config = {
		};

		if (settings) {
			$.extend(config, settings);
		}

		this.each(function () {
			var table = $(this);
			var subrow = table.find('.table-general__subrow');
			var subrowParent = table.find('.table-general__subrow-parent');
			var customSelect = table.find('.custom-select');
			// var select = customSelect.find('select');

			// Updates row classes to change color
			var updateRow = function (select) {
				var optionValue = select.find('option:selected').val();
				var row = select.closest('tr');

				row.removeClass();

				switch (optionValue) {
					case 'ordered-from-supplier':
						row.addClass('table-general__row--yellow');
						break;
					case 'refilled':
						row.addClass('table-general__row--positive');
						break;
				}
			};

			table.find('tbody tr:nth-child(even)').addClass('table-general--even');

			if (subrowParent.hasClass('table-general--even')) {
				subrow.each(function () {
					$(this).addClass('table-general--even');
				});
			}
			else {
				subrow.each(function () {
					$(this).removeClass('table-general--even');
				});
			}

			customSelect.each(function () {
				updateRow($(this));
			});

		});
	};

}(jQuery));