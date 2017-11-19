(function ($) {
	'use strict';

	// init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.autocomplete', context).complete();
	});

	// Replacing the built in function in jQuery Ui autocomplete to be able to highligt
	// search string in results.
	$.ui.autocomplete.prototype._renderItem = function (ul, item) {
		var re = new RegExp(this.term, 'i');
		var t = '';
		if (re.test(item.label)) {
			t = item.label.replace(re, '<b>' + '$&' + '</b>');
		}
		else {
			var startPos = netr.latinise(item.label).toLowerCase().indexOf(netr.latinise(this.term).toLowerCase());

			// Notice that the string is normalized/latinised before measured, because it might get shorter when normalized
			// This also makes the positions inaccurate.
			// TODO: Fix inaccurate highlighting.
			var endPos = startPos + netr.latinise(this.term).length;

			t = item.label.substr(0, startPos) + '<b>' + item.label.substr(startPos, netr.latinise(this.term).length) + '</b>' + item.label.substr(endPos);
		}

		return $('<li></li>')
			.data('item.autocomplete', item)
			.append('<button class="button button--blank button--block">' + t + '</button>')
			.appendTo(ul);
	};

	$.fn.complete = function (settings) {
		// Here be settings
		var config = {
			autoFocus: true
		};

		if (settings) {
			$.extend(config, settings);
		}

		this.each(function () {

			// Cache is king!
			var cache = {};
			var input = $(this);

			// This is true if we want all the data preloaded, and do the filtering front-end
			if (input.data('precache') === true) {

				$.getJSON(input.data('autocomplete'), function (data) {
					input.autocomplete({
						minLength: 1,
						delay: 0,
						autoFocus: config.autoFocus,
						select: function () {
							input.closest('form').submit();
						},
						source: function (request, response) {
							var term = $.ui.autocomplete.escapeRegex(request.term);

							// Selects words with a match in the beginning of a word.
							var startsWithMatcher = new RegExp('(?:^|\\s)' + term, 'ig');
							var startsWith = $.grep(data, function (value) {
								return startsWithMatcher.test(value) || startsWithMatcher.test(netr.latinise(value)) ;
							});

							// Selects words with a match _in_ a word.
							var containsMatcher = new RegExp(term, 'ig');
							var contains = $.grep(data, function (value) {

								// Returns those that are not in the beginning of a word
								return $.inArray(value, startsWith) < 0 && (containsMatcher.test(value) || containsMatcher.test(netr.latinise(value)));
							});

							// Returns both matches in and in beginning of words.
							response(startsWith.concat(contains));
						}
					});
				});
			}
			// a new post for every new search-term.
			else {
				input.autocomplete({
					minLength: 2,
					autoFocus: config.autoFocus,
					select: function () {
						input.closest('form').submit();
					},
					source: function (request, response) {
						var term = request.term;
						if (term in cache) {
							response(cache[term]);
							return;
						}
						$.getJSON(input.data('autocomplete'), request, function (data) {
							cache[ term ] = data;
							response(data);
						});
					}
				});
			}
		});
	};
}(jQuery));