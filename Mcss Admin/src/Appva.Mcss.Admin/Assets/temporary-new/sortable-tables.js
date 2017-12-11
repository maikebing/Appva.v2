/**
 * Scrollable tables
 */
(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.sortable-table', context).sortableTables();
	});

	$.fn.sortableTables = function (settings) {
		var config = {
			sortButtonClass: 'sortable-table__direction',
			descendingClass: 'sortable-table__descending',
			activeButtonClass: 'sortable-table__direction--active'
		};

		if (settings) {
			$.extend(config, settings);
		}

		this.each(function () {
			var table = $(this);
			var sortButtons = table.find('.' + config.sortButtonClass);
			var liveInfo = $('<div />', {
				'class': 'visually-hidden',
				'aria-live': 'polite'
			});
			table.after(liveInfo);
			table.on('click', '.' + config.sortButtonClass, function (e) {
				e.preventDefault();
				var button = $(this);
				var reverse = button.hasClass(config.descendingClass);
				var tableHeader = button.closest('th');

				// which column are we in (zero based)
				var col = tableHeader.index();

				// add active class
				sortButtons.removeClass(config.activeButtonClass);
				button.addClass(config.activeButtonClass);

				// add ARIA-sort
				table.find('th').removeAttr('aria-sort');
				button.closest('th').attr('aria-sort', reverse ? 'ascending' : 'descending');

				// add live message
				// Select only text nodes in the table head that ar immediate children of the th.
				// and remove extra spaces
				var sortedByText = tableHeader.contents().filter(function () {
					return this.nodeType === 3;
				})[0].nodeValue.replace(/\s+/g, ' ').trim();
				var sortDirectionText = reverse ? netr.string.translate('sortableTables.ascending') : netr.string.translate('sortableTables.descending');
				liveInfo.html(netr.string.translate('sortableTables.sortedBy') + ' ' + sortedByText  + ': ' + sortDirectionText);

				// the plus converts the bool to 1 or 0
				sortTable(table[0], col, +reverse);
			});
		});

	};

	function sortTable(table, col, reverse) {
		var tb = table.tBodies[0]; // use `<tbody>` to ignore `<thead>` and `<tfoot>` rows
		var tr = Array.prototype.slice.call(tb.rows, 0); // put rows into array
		var i;

		reverse = -((+reverse) || -1);
		tr = tr.sort(function (a, b) { // sort rows
			return reverse // `-1 *` if want opposite order
				* (a.cells[col].textContent.trim() // using `.textContent.trim()` for test
					.localeCompare(b.cells[col].textContent.trim())
				);
		});
		for (i = 0; i < tr.length; ++i) {
			tb.appendChild(tr[i]);  // append each row in order
		}
	}

}(jQuery));