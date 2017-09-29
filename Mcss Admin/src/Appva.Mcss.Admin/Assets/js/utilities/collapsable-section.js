(function ($) {
	'use strict';

	// Init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.collapsable-section', context).collapsableSection();
	});

	// Collapsable Section!
	$.fn.collapsableSection = function (settings) {
		var config = {
			headingSelector: '.collapsable-section-trigger',
			contentSelector: '.collapsable-section-content',
			contentIdBase: 'collapsable-section-',
			collapsedClass: 'collapsed',
			collapseIcon: 'angle-arrow-up',
			expandedClass: 'expanded',
			expandIcon: 'angle-arrow-down'
		};

		if (settings) {
			$.extend(config, settings);
		}

		this.each(function (index) {
			var section = $(this);
			var header = section.find(config.headingSelector);
			var content = section.find(config.contentSelector);
			var button;
			var iconExpand = $('<svg class="icon icon--ml" aria-hidden="true"><use xlink:href="/Assets/images/icons/icon.sprite.svg#' + config.expandIcon + '-icon"></use></svg>');
			var iconCollapse = $('<svg class="icon icon--ml" aria-hidden="true"><use xlink:href="/Assets/images/icons/icon.sprite.svg#' + config.collapseIcon + '-icon"></use></svg>');

			if (header.length && content.length) {
				// Switch class names and toggle the button's aria-expanded attribute
				var toggleContent = function () {
					if (section.hasClass(config.collapsedClass)) {
						section.removeClass(config.collapsedClass).addClass(config.expandedClass);
						button.attr({
							'aria-expanded': 'true'
						});
						iconExpand.detach();
						button.append(iconCollapse);

						// To trigger any "empty"-messages.
						content.trigger('visibilitychange');
					}
					else {
						section.removeClass(config.expandedClass).addClass(config.collapsedClass);
						button.attr({
							'aria-expanded': 'false'
						});
						iconCollapse.detach();
						button.append(iconExpand);
					}
				};

				// Make sure the content element has an id for the button to reference
				if (!content.attr('id')) {
					content.attr('id', config.contentIdBase + index);
				}

				// Wrap the header's content in a button that toggles the content
				button = $('<button>', {
					'type': 'button',
					'aria-expanded': function () {
						return section.hasClass(config.collapsedClass) ? 'false' : 'true';
					},
					'class': 'button button--blank collapsable-section__button',
					'aria-controls': content.attr('id'),
					'html': header.html(),
					'click': toggleContent
				});

				header.html(button);
				button.append(iconExpand);

			}
		});
	};

}(jQuery));