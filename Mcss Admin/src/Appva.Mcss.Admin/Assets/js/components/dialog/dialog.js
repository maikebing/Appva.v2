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
        // var self = this;
        // var closeIcon = '<svg class="icon" aria-hidden="true"><use xlink:href="/project/images/icons/icon.sprite.svg#x-icon"></use></svg>';

        this.options = $.extend({
            // Extra dialog class
            extraClass: '',
            showDialogTitle: true,
            closeText: netr.string.translate('dialog.close')
        }, options || {});

        // No previous dialog has been created
        if (!container) {
            // Create container
            container = $('<div>', {
                id: 'dialogs'
            }).appendTo('body');
        }

        // Create dialog
        this.dialogElement = $('<div>', {
            'class': 'dialog',
            'id': 'dialog-' + z,
            'role': 'dialog'
        });

        this.dialogElement.addClass(this.options.extraClass);

        // Create and append content container
        this.contentElement = $('<div>', {
            'class': 'dialog__content',
            'aria-live': 'assertive'
        }).appendTo(this.dialogElement);

        // // Create and append close button
        // this.closeElement = $('<button>', {
        // 	'type': 'button',
        // 	'class': 'dialog__close',
        // 	'aria-label': this.options.closeText,
        // 	'html': '<span class="visually-hidden">' + this.options.closeText + '</span>',
        // 	'click': function (e) {
        // 		e.preventDefault();
        // 		self.close();
        // 	}
        // }).prependTo(this.dialogElement);

        dialogs.push(this);
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
        setTitle: function (title, symbol) {
            var titleContainer = $('<div class="dialog__title"></div>');
            if (this.options.showDialogTitle) {
                // Visible dialog title
                this.dialogElement.attr('aria-labelledby', 'dialog-title-' + (z - 1));
                this.dialogElement.find('.dialog__title').remove();
                $('<div class="dialog__symbol"><span>' + symbol + '</span></div>').appendTo(titleContainer);
                $('<div class="dialog__header"><h3 id="dialog-title-' + (z - 1) + '">' + title + '</h3></div>').appendTo(titleContainer);
                titleContainer.prependTo(this.dialogElement);
            }
            else {
                // Visually hidden dialog title
                this.dialogElement.attr('aria-label', title);
            }
        },
        open: function () {
            var self = this;

            container.appendTo('body');
            this.dialogElement.appendTo(container);
            this.position();

            //Add bg
            $('body').addClass('dialog-bg');

            // Add tabindex attribute so the dialog can gain focus
            this.dialogElement.attr('tabindex', '-1').focus();

            // Prevent tabbing outside the dialog
            $(document).on('focus.netrdialog', '*', function (e) {
                if (!$.contains(self.dialogElement.get(0), e.target)) {
                    e.stopPropagation();


                    // Put focus on the first focusable element in the dialog

                    //self.dialogElement.find('input, select, textarea, button, object, a, [tabindex]').filter(function () {
                    //    var element = $(this);
                    //    return (!element.is(':disabled') && (element.attr('tabindex') !== '-1'));
                    //}).eq(0).focus();

                }
            });
            // Close the dialog when the ESC key is pressed
            $(document).on('keydown.netrdialog', function (e) {
                if (e.keyCode === 27) { // ESC
                    // Close all dialogs
                    $.each(dialogs, function () {
                        this.close();
                    });
                }
            });

            // Close the dialog when something outside it is clicked or tapped
            // `$(e.target).data('event') === 'click'` is to prevent the dialog from closing when pressing the
            // next/prev month buttons on a datepicker.
            // $(document).bind('click.netrdialog touchend.netrdialog', function (e) {
            // 	if (!jQuery.contains(self.dialogElement.get(0), e.target) && (self.dialogElement.get(0) !== e.target)) {
            // 		if ($('.ui-datepicker').length) {
            // 			if (jQuery.contains($('#ui-datepicker-div').get(0), e.target) || ($('#ui-datepicker-div').get(0) === e.target) || $(e.target).data('event') === 'click') {
            // 				return;
            // 			}
            // 		}

            // 		// close dialog
            // 		e.preventDefault();
            // 		self.close();
            // 	}
            // });

            // Trigger open event
            this.dialogElement.trigger('open.netrdialog', this);
        },
        close: function () {
            // Trigger close event
            var close_event = new $.Event('close.netrdialog');
            this.dialogElement.trigger(close_event);
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
                        closeText: options.closeText
                    }));

                    dialog.dialogElement.bind({
                        'close.netrdialog': function (e) {
                            // Remove events from document
                            $(document).off('focus.netrdialog', '*').off('keydown.netrdialog click.netrdialog touchend.netrdialog');

                            //Remove bg
                            $('body').removeClass('dialog-bg');

                            if (!e.isDefaultPrevented()) {
                                $(this).detach();
                                container.detach();
                            }

                            // Let the same event bubble away
                            // to the connected element.
                            element.trigger(e, dialog);

                            if (!e.isDefaultPrevented()) {
                                element.focus();

                                if (!options.persistent) {
                                    dialog.dispose();
                                    // Remove references to dialog
                                    element.data('netrdialog', dialog = null);
                                }
                                else {
                                    // Remove listener for clicks outside dialog
                                    $(document).off('click.netrdialog touchend.netrdialog');
                                }
                            }
                        },
                        'open.netrdialog': function (e) {
                            // Let the same event bubble away
                            // to the connected element.
                            element.trigger(e, dialog);
                        },
                        'load.netrdialog': function (e) {
                            if (options.hijackForms) {
                                var form = dialog.contentElement.find('form');
                                var button;

                                if (form.attr('enctype') !== 'multipart/form-data') {
                                    // Observe button clicks
                                    form.on('click', '[type=submit]', function () {
                                        button = $(this);
                                    });

                                    // Observe submit event
                                    form.submit(function (e) {
                                        e.preventDefault();

                                        // Remove any placeholder texts before serializing.
                                        form.find('.placeholder').val(function (index, current_value) {
                                            if (current_value === $(this).attr('placeholder')) {
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
                                                    dialog.setContent(data);

                                                    // Fire Callbacks!
                                                    callbacks.fire(data, dialog);
                                                }
                                            },
                                            error: function () {
                                                dialog.setContent($(options.errorMessage));
                                            },
                                            complete: function () {
                                                form.removeClass('spinner');
                                            }
                                        });
                                    });
                                }
                            }

                            // Trigger new content and find and bind in-content-close-buttons.
                            dialog.contentElement.trigger('newcontent');
                            dialog.contentElement.on('click', '[data-dialog-close]', function () {
                                dialog.close();
                            });

                            // Let the same event bubble away
                            // to the connected element.
                            element.trigger(e, dialog);
                        }
                    });
                }

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
                            error: function () {
                                dialog.setContent($(options.errorMessage));
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
                        var symbol = element.data('dialog-symbol');
                        if (title && symbol) {
                            dialog.setTitle(title, symbol);
                        }
                        else if (title) {
                            dialog.setTitle(title, '!');
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

}(jQuery));