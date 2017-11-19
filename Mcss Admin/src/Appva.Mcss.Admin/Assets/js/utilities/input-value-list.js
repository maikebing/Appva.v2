(function ($) {
	'use strict';

	// TODO: Consider making this work better without JS. Maybe comma separated?

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.input-value-list', context).inputValueList();
		$('[data-soft-post]', context).softPost();
	});

	// Makes inputs post to javascript function using the "softpost"-event
	// TODO: Soft post as a more universal concept?
	$.fn.softPost = function () {
		var button = $(this);
		button.on('click', function (e) {
			e.preventDefault();
			$('#' + button.data('soft-post')).trigger('softpost');
		});
	};

	// Input Value List!
	$.fn.inputValueList = function (settings) {
		var config = {
			removeButton: $('<button class="button button--ui button--inline" type="submit"><svg class="icon icon--mr" aria-hidden="true"><use xlink:href="/Assets/images/icons/icon.sprite.svg#x-icon"></use></svg> Ta bort</button>')
		};

		if (settings) {
			$.extend(config, settings);
		}

		// Add to list
		function addValue(list, input, counter) {
			var listItem = $('<li />');
			var hiddenInput = $('<input />', {
				type: 'hidden',
				value: input.val(),
				id: list.attr('id') + '_hidden-input_' + counter,
				name: list.attr('id') + '_hidden-input_' + counter
			});

			// We will be able to use this data value to find the input that added it.
			// Remember that this value will not be visible in the markup:)
			hiddenInput.data('input-value-list-added-by', input.attr('id'));

			// adding the text
			listItem.html(input.val());

			// adding the remove-button
			listItem.append(config.removeButton.clone(), hiddenInput);

			// add list item to list.
			list.append(listItem);

			// Is it an added date from a datepicker?
			if (input.hasClass('datepicker')) {
				// If no dates are selected, create an empty array.
				var newSelectedDates = input.data('datepicker-selected-dates') || [];

				// Add new date to list of selected dates
				newSelectedDates.push(input.val());
				input.data('datepicker-selected-dates', newSelectedDates);
			}

			counter = counter + 1;
			return counter;
		}

		// Remove from list
		function removeValue(list, input, hiddenInput) {
			if (input.hasClass('datepicker')) {

				// Here we enable the value in the calendar again

				// find what value we are enabling
				var value = hiddenInput.val();

				// Get the selected values
				var newSelectedDates = input.data('datepicker-selected-dates');

				// We go through the array, returning all the values that _doesn't_ match
				// our value. Which are then added to the new array.
				newSelectedDates = newSelectedDates.filter(function (e) { return e !== value; });

				// Update the selected dates
				input.data('datepicker-selected-dates', newSelectedDates);

				// Get the current list item.
				var currentListItem = hiddenInput.closest('li');

				// Focus the previous button - if any
				if (currentListItem.prev('li').length) {
					currentListItem.prev('li').find('button').focus();
				}
				// if none select the next
				else if (currentListItem.next('li').length) {
					currentListItem.next('li').find('button').focus();
				}
				// otherwise focus the input that added the list item.
				else {
					input.focus();
				}
				hiddenInput.closest('li').remove();
			}

		}

		this.each(function () {
			// We work relative from the list to (in the future) be able to have multiple inputs controlling it.
			var list = $(this);

			// Message to show if list is empty.
			var emptyListMessage = $('<div class="system-message system-message--info">' + netr.string.translate('inputValueList.empty') + '</div>');

			// hide it in advance.
			emptyListMessage.hide();
			list.append(emptyListMessage);
			// Sets the counter to the number of elements already in the list.
			var counter = list.find('li').length;

			// Show empty message if nothing's added to list.
			if (counter === 0) {
				emptyListMessage.show();
			}

			// Select all inputs with a 'data-input-value-list' value that corresponds
			// to the ID of our list.
			// These are the input that can add values to our list.
			var inputs = $('[data-input-value-list="' + list.attr('id') + '"]');

			// We support (or will support) multiple controlling inputs
			inputs.each(function () {
				var input = $(this);

				// Select the corresponding submit-button by data attr input-value-list-button'
				// $('#' + input.data('input-value-list-button')).on('click', function (e) {
				input.on('softpost', function () {
					var valid = true;
					// TODO: This check doesn't seems to work...
					if (typeof input.valid === 'function') {
						valid = input.valid();
					}
					if (valid === true) {
						var hiddenInputAlreadyInList = list.find('[value="' + input.val() + '"]');

						// are there any li with data-input-value-list-added-by that is same as the inputs ID and have the same value?
						// TODO: Adding an already added value with the text input should not remove the previously added value.
						var addedBefore = hiddenInputAlreadyInList.filter(function () {
							// case sensitive..?
							return $(this).data('input-value-list-added-by') === input.attr('id');
						}).length;

						if (input.val() !== '' && !addedBefore) {

							// Run function to add value to list.
							counter = addValue(list, input, counter);

							// Since we know at least one will be added after this, we can with certainty
							// know that the empty-list-message isn't needed.
							emptyListMessage.hide();
						}
						else if (input.val() !== '') {

							// Run function to add remove value from list.
							// Note that the counter doesn't update since we only increase the counter.
							removeValue(list, input, hiddenInputAlreadyInList);

							// Check if there are any list items left, if not: show empty-list-message
							if (list.find('li').length === 0) {
								emptyListMessage.show();
							}
						}

						// Empty the input field.
						input.val('');
					}
				});

			});

			// This is a click on the remove button.
			// Utilise bubbling to avoid binding the event on each separate button
			list.on('click', 'button', function (e) {
				var button = $(this);

				// we don't want no postin'
				e.preventDefault();

				// find what input element added the list item.
				var addedByInput = $('#' + button.siblings('input').data('input-value-list-added-by'));

				// Find the hidden input in the list item.
				var hiddenInput = button.siblings('input[type="hidden"]');
				removeValue(list, addedByInput, hiddenInput);

				// Check if there are any list items left, if not: show empty-list-message
				if (list.find('li').length === 0) {
					emptyListMessage.show();
				}
			});
		});
	};

}(jQuery));