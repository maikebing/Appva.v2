/**
 * Updates a section with content, either by posting a form, or by clicking a link
 * @requires netr.uri
 */
(function ($) {
	'use strict';

	// init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('[data-update-section-target]', context).updateTarget();
	});

	$.updateTarget = function (settings) {
		// Here be settings
		var config = {
			url: null,
			target: null,
			data: null
		};

		if (settings) {
			$.extend(config, settings);
		}

		// To be able to use the function without an element>
		// eg: $.updateTarget('{}');
		var target = config.target;
		$.getFragment({
			url: config.url,
			data: config.data,
			success: function (data) {
				if (data.length) {
					target.find('.system-message').remove();
					target.empty().append($(data));
					data.trigger('newcontent');
				}
			},
			error: function (jqXHR) {
				var errorMsg = $('<div class="system-message system-message--error"><div class="system-message__heading"><h2>' + netr.string.translate('updateSection.error.formHeading') + '</h2></div><p>' + netr.string.translate('updateSection.error.instruction') + '</p><ul class="system-message__listing"><li>' + netr.string.translate('updateSection.error.errorCode') + ': ' + jqXHR.status + '</li><li>' + netr.string.translate('updateSection.error.errorMessage') + ': ' + jqXHR.statusText + '</li></ul></div>');
				target.find('.system-message').remove();
				target.append(errorMsg);
			},
			complete: function () {
				target.removeClass('spinner');
			}
		});
	};
	$.fn.updateTarget = function (settings) {
		// Here be settings
		var config = {
			url: null,
			target: null,
			data: null
		};
		var url;
		if (settings) {
			$.extend(config, settings);
		}

		this.each(function () {
			// Target area that will be updated
			var target = $('#' + $(this).data('update-section-target'));
			if (target.length === 0) {
				// If there is no target, it falls back to standard non-js behaviour.
				return;
			}
			target.attr('aria-live', 'polite').attr('role', 'region');

			// Is this a form?
			if ($(this).is('form')) {
				var form = $(this);
				form.attr('aria-controls', form.data('update-section-target'));
				url = new netr.URI(form.attr('action'));
				if (!url.fragment) {
					throw new Error('URL passed to $.getFragment is missing fragment.');
				}

				if (form.data('update-section-autopost')) {
					form.find('input, select').on('change', function () {
						form.submit();
					});
				}

				// When form is being submitted
				form.submit(function (event) {
					// Stop form from submitting normally
					event.preventDefault();

					// Was it a submit button that triggered the event?
					var submitButton = $(document.activeElement);
					if (
						// there is an activeElement at all
						submitButton.length &&

						// it's a child of the form
						form.has(submitButton) &&

						// it's really a submit element
						submitButton.is('button[type="submit"], input[type="submit"], input[type="image"]') &&

						// it has a "name" attribute
						submitButton.is('[name]')
					) {
						// add hidden input to get name of submit
						var hiddenInput = $('<input />', {
							type: 'hidden',
							value: submitButton.attr('value'),
							name: submitButton.attr('name')
						});
						submitButton.append(hiddenInput);
					}

					// Is jQuery validate active on the form?
					if (typeof form.validate === 'function' && form.attr('no-validate')) {
						if (!form.valid()) {
							return false;
						}
					}
					target.addClass('spinner');
					var formData = form.serialize();
					form.closest('.dialog').trigger('close.netrdialog');
					$.getFragment({
						url: form.attr('action'),
						data: formData,
						success: function (data) {
							if (data.length) {
								form.find('.system-message').remove();
								target.empty().append($(data));
								data.trigger('newcontent');
							}
						},
						error: function (jqXHR) {
							var errorMsg = $('<div class="system-message system-message--error"><div class="system-message__heading"><h2>' + netr.string.translate('updateSection.error.formHeading') + '</h2></div><p>' + netr.string.translate('updateSection.error.instruction') + '</p><ul class="system-message__listing"><li>' + netr.string.translate('updateSection.error.errorCode') + ': ' + jqXHR.status + '</li><li>' + netr.string.translate('updateSection.error.errorMessage') + ': ' + jqXHR.statusText + '</li></ul></div>');
							form.find('.system-message').remove();
							form.append(errorMsg);
						},
						complete: function () {
							target.removeClass('spinner');
						}
					});
				});
			}
			// It's a link
			else if ($(this).is('a')) {
				var link = $(this);
				link.attr('aria-controls', link.data('update-section-target'));
				url = new netr.URI(link.attr('href'));
				if (!url.fragment) {
					throw new Error('URL passed to $.getFragment is missing fragment.');
				}

				link.on('click', function (e) {
					e.preventDefault();
					target.addClass('spinner');
					$.getFragment({
						url: link.attr('href'),
						success: function (data) {
							if (data.length) {
								// We just want the content, to be able to link the same page and
								// target, without a duplicate target element.
								target.empty().append(data.contents());
								target.trigger('newcontent');
							}
						},
						error: function (jqXHR) {
							var errorMsg = $('<div class="system-message system-message--error"><div class="system-message__heading"><h2>' + netr.string.translate('updateSection.error.linkHeading') + '</h2></div><p>' + netr.string.translate('updateSection.error.instruction') + '</p><ul class="system-message__listing"><li>' + netr.string.translate('updateSection.error.errorCode') + ': ' + jqXHR.status + '</li><li>' + netr.string.translate('updateSection.error.errorMessage') + ': ' + jqXHR.statusText + '</li></ul></div>');
							target.find('.system-message').remove();
							target.append(errorMsg);
						},
						complete: function () {
							target.removeClass('spinner');
						}
					});
				});
			}
		});
	};
}(jQuery));