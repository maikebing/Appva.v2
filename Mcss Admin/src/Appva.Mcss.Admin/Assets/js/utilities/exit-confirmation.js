(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('form', context).formChange();
	});

	// If form is changed
	$.fn.formChange = function (settings) {
		var config = {
		};

		if (settings) {
			$.extend(config, settings);
		}

		var otherButtons = $('a, button').not('form a, form button, open-in-dialog');

		this.each(function () {
			var form = $(this);
			var clearForm = form.serialize();
			var hasInput = false;
			var submit = form.find($(':submit').last());
			var cancel = submit.parent().find($('a, button').not(submit));
			var buttons = otherButtons.add(cancel);

			form.on('change input', function () {
				hasInput = form.serialize() !== clearForm;
			});

			buttons.on('click', function (e) {
				if (hasInput === true) {
					e.preventDefault();
					var tag = $(this).prop('tagName');
					var href = '#';

					if (tag === 'A') {
						href = $(this).attr('href');
					}

					var title = 'Lämna formulär';
					var message = 'Du har inte sparat informationen i formuläret. Vill du verkligen lämna sidan?';
					var symbol = '!';
					var leaveBtn;
					var dialog = $('<div class="dialog-bg"><div class="dialog--form-warning" role="dialog" tabindex="-1"><div class="dialog__title"><div class="dialog__symbol"><span>' + symbol + '</span></div><div class="dialog__header"><h3>' + title + '</h3></div></div><div class="dialog__content" aria-live="assertive"><div class="width-limiter width-limiter--xs"><p>' + message + '</p><div class="form-controls form-controls--align-end form-controls--no-margin">&nbsp;<a id="form-stay" class="button button--secondary">Avbryt</a></div></div></div></div></div>');

					dialog.appendTo($('body'));
					dialog.find($('#form-stay')).click(function () {
						dialog.remove();
					});

					leaveBtn = $('<a id="form-leave" class="button button--negative" href="' + href + '">Lämna sida</a>');

					leaveBtn.appendTo(dialog.find($('.form-controls')));
					leaveBtn.click(function () {
						hasInput = false;
						dialog.remove();
					});
				}
			});
		});
	};

}(jQuery));