(function ($) {
	'use strict';

	/* ============================================
	 * data-toggle-targets = A JSON array of id's to elements to toggle. (attr on input)
	 * data-toggle-by-input = what happens when activated (enabling, disabling, hiding, showing) Defaults to show if not set. (attr on target)
	 * data-toggle-values =  A JSON array of values to trigger. (true on value x), needed for radio, and selects (attr on target)
	 * ===========================================*/

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.toggle-target', context).toggleByInput();
	});

	// toggle by input!
	$.fn.toggleByInput = function (settings) {
		var config = {
			valueDataAttribute: 'toggle-values',
			toggleTargetsDataAttribute: 'toggle-targets',
			toggleModeDataAttribute: 'toggle-by-input'
		};

		if (settings) {
			$.extend(config, settings);
		}

		function disableInputs(element) {
			element.find('input, select, button').prop('disabled', true);
		}
		function enableInputs(element) {
			element.find('input, select, button').prop('disabled', false);
		}

		/**
		 * inputs element, hides/shows/disable/enable it depending on attr.
		 * @param  {object} activate   List of jQuery objects to activate
		 * @param  {object} deactivate List of jQuery objects to deactivate
		 */
		function toggleElements(activate, deactivate) {
			// enables using multiple elements as input
			activate.each(function () {
				var self = $(this);

				// If no toggle mode is defined, it defaults to 'show'
				var toggleMode = self.data(config.toggleModeDataAttribute) || 'show';

				switch (toggleMode) {
					case 'hide':
						self.hide();
						break;
					case 'show':
						self.fadeIn();
						break;
					case 'disable':
						disableInputs(self);
						break;
					case 'enable':
						enableInputs(self);
						break;
				}
			});

			deactivate.each(function () {
				var self = $(this);

				// If no toggle mode is defined, it defaults to 'show'
				var toggleMode = self.data(config.toggleModeDataAttribute) || 'show';

				// Like the switch above, only backwards.
				switch (toggleMode) {
					case 'hide':
						self.fadeIn();
						break;
					case 'show':
						self.hide();
						break;
					case 'disable':
						enableInputs(self);
						break;
					case 'enable':
						disableInputs(self);
						break;
				}
			});
			deactivate.trigger('visibilitychange');
			activate.trigger('visibilitychange');
		}

		this.each(function () {
		    var formControl = $(this);
		    //console.log($.makeArray(formControl.data(config.toggleTargetsDataAttribute)).join(', #'));
		    var targets = $('#' + $.makeArray(formControl.data(config.toggleTargetsDataAttribute)).join(', #'));

			// add Aria attributes
		    formControl.attr('aria-controls', $.makeArray(formControl.data(config.toggleTargetsDataAttribute)).join(' '));
			// TODO: Find an alternative to below to make screenreaders read what changed.
			// targets.attr('aria-live', 'polite');

			// Checkboxes
			if (formControl.attr('type') === 'checkbox') {
				formControl.on('change', function () {
					if (formControl.prop('checked') === true) {
						toggleElements(targets, $());
					}
					else {
						toggleElements($(), targets);
					}
				});
			}

			// Radiobuttons
			if (formControl.is('fieldset')) {
				formControl.on('change', function () {
					var value = formControl.find('input:checked').val();
					var toActivate = targets.filter(function () {
						// Returns elements that have a data-value whose array contains value (mind bending)
					    try {
					        return $(this).data(config.valueDataAttribute).indexOf(value) !== -1;
					    } catch (e) {
					        console.log($(this));
					    }
					});
					toggleElements(toActivate, targets.not(toActivate));
				});
			}

			// Selects
			if (formControl.is('select')) {
				formControl.on('change', function () {
					var value = $(this).val();
					var toActivate = targets.filter(function () {
					    // Returns elements that have a data-value whose array contains value (mind bending)
					    try {
					        return $(this).data(config.valueDataAttribute).indexOf(value) !== -1;
					    } catch (e)
					    {
					        console.log($(this));
					    }
					});
					toggleElements(toActivate, targets.not(toActivate));
				});
			}
		});
		// Trigger change to activate on load
		$(this).trigger('change');
	};

}(jQuery));