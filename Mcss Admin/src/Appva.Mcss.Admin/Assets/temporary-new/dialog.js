(function ($) {
	'use strict';

	// Open in dialog on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.open-in-dialog', context).netrdialog();
	});

	// Dialog!

	// All added dialogs
	var dialogs = [];

	// z-index of topmost dialog
	var z = 1;

	// Dialog container
	var container;

	/**
	Dialog
	* TODO: abort ajax call on click on shield?
	*/
	var Dialog = function (options) {
		var self = this;
		// TODO: Change variable name to trigger or something.
		var closeIcon = '<svg class="icon" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#x-icon"></use></svg>';

		// Default options
		self.options = $.extend({
			// Extra dialog class
			extraClass: '',
			showDialogTitle: true,
			closeText: netr.string.translate('dialog.close'),
			triggerElement: $(),
		}, options || {});

		// No previous dialog has been created
		if (!container) {
			// Create container
			container = $('<div>', {
				id: 'dialogs'
			}).appendTo('body');

			// Close the dialog when the ESC key is pressed
			$(document).on('keydown', function (e) {
				if (e.keyCode === 27) { // ESC
					// Close all dialogs
					closeTopmost();
				}
			});
		}

		// Create dialog
		self.dialogElement = $('<div>', {
			'class': 'dialog',
			'id': 'dialog-' + z,
			'role': 'dialog'
		});

		// add any extra classes
		self.dialogElement.addClass(self.options.extraClass);

		// Create and append content container
		self.contentElement = $('<div>', {
			'class': 'dialog__content',
			'aria-live': 'assertive'
		}).appendTo(self.dialogElement);

		// Create and append close button
		self.closeElement = $('<button>', {
			'type': 'button',
			'class': 'dialog__close no-exit-conf button button--icon-only',
			'aria-label': self.options.closeText,
			'html': '<span class="visually-hidden">' + self.options.closeText + '</span>' + closeIcon,
			'click': function (e) {
				e.preventDefault();
				self.close();
			}
		}).prependTo(self.dialogElement);
		self.dialogElement.bind({
			'close.netrdialog': function (e) {
				// Remove events from document
				$(document).off('focus.netrdialog', '*').off('keydown.netrdialog click.netrdialog touchend.netrdialog');

				//Remove bg
				$('body').removeClass('dialog-bg');

				// TODO: find out the purpose of this...
				if (!e.isDefaultPrevented()) {
					$(self).detach();
					container.detach();
				}

				// Let the same event bubble away
				// to the connected element.
				self.options.triggerElement.trigger(e, self);

				if (!e.isDefaultPrevented()) {
					self.options.triggerElement.focus();

					if (!self.options.persistent) {
						self.dispose();
						// Remove references to dialog
						self.options.triggerElement.data('netrdialog', self = null);
						container.remove();
					}
					else {
						// Remove listener for clicks outside dialog
						$(document).off('click.netrdialog touchend.netrdialog');
					}
				}
			},
			'closeTopmost.netrdialog': function () {
				closeTopmost(self);
			},
			'open.netrdialog': function (e) {
				// Let the same event bubble away
				// to the connected element.
				self.options.triggerElement.trigger(e, self);
			},
			'load.netrdialog': function (e) {
				var form = self.contentElement.find('form');

				if (self.options.hijackForms) {

					var button;
					if (form.attr('enctype') !== 'multipart/form-data') {
						// Observe button clicks
						form.on('click', '[type=submit]', function () {
							button = $(self);
						});

						// Observe submit event
						form.submit(function (e) {
							e.preventDefault();

							// Remove any placeholder texts before serializing.
							form.find('.placeholder').val(function (index, current_value) {
								if (current_value === $(self).attr('placeholder')) {
									return '';
								}
								else {
									return current_value;
								}
							});

							var data = form.serialize();

							if (button && button.length) {
								data += ('&' + button.attr('name') + '=' + encodeURIComponent(button.val()));
							}
							form.addClass('spinner');
							$.getFragment({
								url: form.attr('action'),
								type: (form.attr('method') || 'post'),
								data: data,
								success: function (data) {
									if (data.length) {
										self.setContent(data);

										// Fire Callbacks!
										// callbacks.fire(data, dialog);
									}
								},
								error: function () {
									self.setContent($(options.errorMessage));
								},
								complete: function () {
									form.removeClass('spinner');
								}
							});
						});
					}
				}
				// Move heading outside content
				self.contentElement.find('.alert-message').first().prependTo(self.dialogElement).addClass('alert-message--dialog');

				// Trigger new content and find and bind in-content-close-buttons.
				self.contentElement.trigger('newcontent');

				self.contentElement.on('click', '[data-dialog-close]', function (e) {
					e.preventDefault();
					self.close();
				});
				// Let's the event bubble away
				// to the connected element.
				if (self.options.triggerElement !== null) {
					self.options.triggerElement.trigger(e, self);
				}
			}
		});
		dialogs.push(self);
	};

	Dialog.prototype = {
		dispose: function () {
			var self = this;
			// Remove elements
			this.dialogElement.remove();

			// Remove from array
			$.each(dialogs, function (index, dialog) {
				if (dialog === self) {
					dialogs.splice(index, 1);
					// Exit each loop
					return false;
				}
			});
		},
		setContent: function (content) {
			this.contentElement.empty().append(content);
			this.position();
			this.dialogElement.trigger('load.netrdialog', this);
		},
		setTitle: function (title, theme, icon) {
			if (this.options.showDialogTitle) {
				// Visible dialog title
				this.dialogElement.attr('aria-labelledby', 'dialog-title-' + (z - 1));
				this.dialogElement.find('.alert-message.alert-message--dialog').remove();
				var dialogTitle = $('<div class="alert-message alert-message--dialog"><h2 id="dialog-title-' + (z - 1) + '" class="alert-message__heading h3">' + title + '</h2></div>');

				// Set a color to title background.
				if (typeof theme === 'string') {
					dialogTitle.addClass('alert-message--' + theme);
				}

				// Add icon to title
				if (typeof icon === 'string') {
					var iconElement = $('<svg class="icon icon--round icon--mr icon--warning" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#' + icon + '-icon"></use></svg>');
					iconElement.prependTo(dialogTitle.find('h2'));
				}

				// Add to top of dialog
				dialogTitle.prependTo(this.dialogElement);
			}
			else {
				// Visually hidden dialog title
				this.dialogElement.attr('aria-label', title);
			}
		},
		open: function () {
			var self = this;
			container.appendTo('body');
			self.dialogElement.appendTo(container);
			self.position();

			//Add bg
			$('body').addClass('dialog-bg');

			// Add tabindex attribute so the dialog can gain focus
			self.dialogElement.attr('tabindex', '-1').focus();

			// Prevent tabbing outside the dialog
			$(document).on('focus.netrdialog', '*', function (e) {
				if (!$.contains(self.dialogElement.get(0), e.target)) {
					e.stopPropagation();
					var elementZValue = self.dialogElement.css('z-index');
					var biggerZValues = false;
					// Finds out if this is the topmost dialog, to prevent eternal
					// recursion
					for (var i = dialogs.length - 1; i >= 0; i--) {
						var zValue = dialogs[i].dialogElement.css('z-index');
						if (zValue > elementZValue) {
							biggerZValues = true;
						}
					}
					if (!biggerZValues) {
						// Put focus on the first focusable element in the dialog
						self.dialogElement.find('input, select, textarea, button, object, a, [tabindex]').filter(function () {
							var element = $(this);
							return (!element.is(':disabled') && (element.attr('tabindex') !== '-1'));
						}).eq(0).focus();
					}
				}
			});

			// Close the dialog when something outside it is clicked or tapped
			// `$(e.target).data('event') === 'click'` is to prevent the dialog from closing when pressing the
			// next/prev month buttons on a datepicker.
			$(document).bind('click.netrdialog touchend.netrdialog', function (e) {

				// Is the click in a dialog?
				var inDialog = $(e.target).closest('.dialog').length || (self.dialogElement.get(0) === e.target);

				// Is the click in a datepicker
				if ($('#ui-datepicker-div').length) {
					var inDatePicker = jQuery.contains($('#ui-datepicker-div').get(0), e.target) || ($('#ui-datepicker-div').get(0) === e.target) || $(e.target).data('event') === 'click';
				}

				if (inDialog || inDatePicker) {
					return;
				}

				closeTopmost();

			});
			// Trigger open event
			this.dialogElement.trigger('open.netrdialog', this);
		},
		close: function () {

			// Trigger close event
			var dirtyForms = this.dialogElement.find('form.exit-confirmation').filter(function () {
				return $(this).data('dirty') === 'true';
			});
			if (dirtyForms.length) {
				// This is an event from exit-confirmation.js that will trigger an exit confirmation dialog.
				dirtyForms.trigger('formclosing');
				return;
			}
			if (ontop(this) !== true) {
				var close_event = new $.Event('close.netrdialog');
				this.dialogElement.trigger(close_event);
			}
			else {
				this.dispose();
			}
		},
		position: function () {
			// Increment maximum z value
			z++;
			this.dialogElement.css({
				top: Math.max($(document).scrollTop() + 20, $(document).scrollTop() + ($(window).height() / 2) - (this.dialogElement.height() / 2)),
				zIndex: z
			});
		}
	};

	/**
	Create an independent dialog
	*/
	$.netrdialog = function (options) {
		return new Dialog(options);
	};

	/**
	Open the source of a link element in a dialog
	@param {Object}  options  Options
	@memberOf $.fn
	*/
	$.fn.netrdialog = function (method, options) {
		if (typeof method === 'string') {
			switch (method) {
				case 'getdialog':
					return this.data('netrdialog');
				case 'setcontent':
					var content = options;
					return this.data('netrdialog').setContent(content);
				case 'settitle':
					return this.data('netrdialog').setTitle(options);
				case 'open':
					this.data('netrdialog').open();
					break;
				case 'close':
					this.data('netrdialog').close();
					break;
				default:
					break;
			}
		}
		else {
			// Set options
			options = $.extend({
				// Should data be loaded every time?
				persistent: false,
				// Hijack any form submissions
				hijackForms: false,
				// Extra dialog class
				extraClass: '',
				// Alt text for the close button image
				closeText: netr.string.translate('dialog.close'),
				// A generic error message
				errorMessage: '<p>' + netr.string.translate('dialog.errorMessage') + '</p>',
				// Set to false to disable the dialog title
				showDialogTitle: true
			}, $.isPlainObject(method) ? method : ($.isPlainObject(options) ? options : {}));

			// Callbacks to run upon success
			// eslint-disable-next-line new-cap
			var callbacks = $.Callbacks();
			if (typeof (options.callback) === 'function') {
				callbacks.add(options.callback);
			}

			this.click(function (e) {
				e.preventDefault();
				var element = $(this);
				var dialog = element.data('netrdialog');

				if (!dialog) {
					// Create a new dialog
					element.data('netrdialog', dialog = new Dialog({
						extraClass: options.extraClass,
						closeText: options.closeText,
						triggerElement: element
					}));
				}

				// If there is no dialog, with content (i.e. it has not been triggered before)
				if (!dialog.contentElement.children().length || !options.persistent) {
					if (element.attr('href').match(/^#/)) {
						// Get element from current page
						// TODO: Should probably clone events too... ..or maybe not? at least prefix any id:s...
						dialog.setContent($(element.attr('href')).clone());
						dialog.open();
					}
					else {
						// Get element by ajax
						$('body').addClass('spinner');
						$.getFragment({
							url: element.attr('href'),
							success: function (data) {
								if (data.length) {
									dialog.setContent(data);
									// Fire callbacks
									callbacks.fire(data, dialog);
								}
							},
							error: function (data) {
								dialog.setContent($(options.errorMessage));
								console.log(data);
								dialog.setTitle(data.status + ', ' + data.statusText, 'warning', 'alert');
							},
							complete: function () {
								$('body').removeClass('spinner');
								dialog.open();
								dialog.contentElement.trigger('newcontent');
							}
						});
					}

					// Add a title if the triggering element has a data-dialog-title attribute
					callbacks.add(function (data, dialog) {
						var title = element.data('dialog-title');
						if (title) {
							dialog.setTitle(title);
						}
					});

				}
			});
		}

		// Open on init?
		if (options.open) {
			this.click();
		}

		return this;
	};

	function ontop(dialog) {
		// Returns the true if element is on top, returns the element that is or false if theres just one dialog
		var dialogElement = (typeof dialog !== 'undefined') ? dialog.dialogElement : $();
		// This will be the topmost dialog
		var topDialog = dialogs[0];

		// The highest z-value of the dialogs
		var highestZ = 0;

		// If there is only one dialog element. Then it's not on top of anything.
		if (dialogs.length === 1) {
			// if there's only one dialog element
			return false;
		}

		for (var i = dialogs.length - 1; i >= 0; i--) {
			// the z value of the current element in the iteration
			// unary plus to convert to string
			var zValue = +dialogs[i].dialogElement.css('z-index');

			// If the iterated z value is bigger than that of the element we're testing against
			if (zValue >= highestZ) {
				topDialog = dialogs[i];
				highestZ = zValue;
			}
		}

		// Is the tested dialog on top?
		if (dialogElement.length && topDialog === dialog) {
			return true;
		}
		else {
			// Returns the element that is topmost
			return topDialog;
		}
	}
	function closeTopmost(dialog) {
		// This is to make the function bail early if there is no active dialogs
		// for example when ESC is pressed without dialogs.
		if (dialogs.length === 0) {
			return;
		}
		var onTopValue = ontop(dialog);
		if (onTopValue === true) {
			// Entered dialog is on top, dispose of that!
			dialog.dispose();
		}
		else if (onTopValue === false) {
			// onTopValue returns false => there is only one dialog. Let's close and clean
			dialogs[0].close();
		}
		else {
			// onTopValue returns the topmost dialog. Let's dispose of that.
			onTopValue.close();
		}
	}

}(jQuery));