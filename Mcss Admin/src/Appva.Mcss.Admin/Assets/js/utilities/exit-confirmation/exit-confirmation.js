(function ($) {
    'use strict';

    // Init on new content
    $('body').on('newcontent', function (e) {
        var context = e.target;
        $('.exit-confirmation', context).exitConfirmation();
    });

    // If form is changed
    $.fn.exitConfirmation = function (settings) {
        var config = {
        };

        if (settings) {
            $.extend(config, settings);
        }

        // Buttons outside form that reloads the page or trigger navigation away from the page. And a little hacky class to prevent
        // some buttons from triggering exit conf.
        var otherButtons = $('a, button').not('form a, form button, open-in-dialog, .no-exit-conf');

        this.each(function () {
            var form = $(this);

            // The form in it's pristine state
            // TODO: check how firefox - with it's habit to remember values - does this. Is this really the clean state?
            var clearForm = form.serialize();

            // Is the form "dirty", ie. is any inputs changed from the initial state
            form.data('dirty', 'false');

            // The last submit button, supposedly the main submit button
            // TODO: thats an assumption, that probably isn't always true...
            var submit = form.find($(':submit').last());

            // Cancel buttons.
            // TODO: See if we can do this some other way, seems a bit fragile
            var cancel = submit.parent().find($('a, button').not(submit));
            var buttons = otherButtons.add(cancel);

            // If anything changes, does the form differ from it's pristine state?
            // if so: Form is dirty.
            form.on('change input', function () {
                form.data('dirty', (form.serialize() !== clearForm).toString());
            });

            function showDialogIfDirty(e) {
                if (form.data('dirty') === 'true' && form.is(':visible')) {
                    var triggeringEvent = e;
                    e.preventDefault();
                    var confirmDialog = $.netrdialog();
                    // TODO: Translate "leave form"
                    var leaveButton = $('<button class="button button--primary button--ml" data-dialog-action="close">Leave form</button>');
                    var stayButton = $('<button class="button button--secondary" data-dialog-action="remain">Stay on form</button>');
                    var content = $('<div><p>Do you want to leave your data?</p></div>');
                    var buttonContainer = $('<div class="form-controls form-controls--align-end form-controls--no-margin"></div>');

                    stayButton.appendTo(buttonContainer);
                    leaveButton.appendTo(buttonContainer);
                    buttonContainer.appendTo(content);

                    confirmDialog.setContent(content);
                    confirmDialog.open();
                    e.stopPropagation();

                    // events
                    confirmDialog.dialogElement.on('click', '[data-dialog-action="close"]', function () {
                        form[0].reset();
                        form.data('dirty', 'false');
                        // close dialog if the form is within one.
                        confirmDialog.close();

                        // setTimeout(function () {
                        // Do what was planned to do!
                        // console.log(triggeringEvent.target);
                        // console.log(triggeringEvent.type);
                        $(triggeringEvent.target).trigger(triggeringEvent.type);
                        // }, 200);
                        // form.trigger('close.netrdialog');

                    });
                    confirmDialog.dialogElement.on('click', '[data-dialog-action="remain"]', function () {
                        confirmDialog.close();
                    });
                }
            }
            // if closing a form
            form.on('formclosing', showDialogIfDirty);
            // If any non submit button is pressede
            buttons.on('click', showDialogIfDirty);
        });
    };

}(jQuery));