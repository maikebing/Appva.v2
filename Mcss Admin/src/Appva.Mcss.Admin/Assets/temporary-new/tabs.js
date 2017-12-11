(function ($) {
	'use strict';

	// init on new content
	$('body').on('newcontent', function (e) {
		var context = e.target;
		$('.tabs--dynamic', context).dynamicTabs();
		$('.tabs--toggler', context).togglerTabs();
	});

	$.fn.dynamicTabs = function () {
		// TODO: Add aria
		var tabs = $(this);
		var target = $('#' + tabs.data('tab-target'));
		var firstTab = tabs.find('.tabs__item a').first();

		tabs.find('.tabs__item a').on('click', function (e) {
			e.preventDefault();
			var self = $(this);

			// Remove selected highlight
			$('.tabs__item--selected', tabs).removeClass('tabs__item--selected');

			// Add highlight to clicked tab
			self.closest('.tabs__item').addClass('tabs__item--selected');
			$.getFragment({
				url: self.attr('href'),
				async: true,
				success: function (data) {
					if (data.length) {
						// The data comes with the wrapping tabtarget element, which will be dup-
						// licated if not unwrapped.
						target.empty().append(data.contents().unwrap());
						target.trigger('newcontent');
					}
				},
				error: function () {
					target.empty().append('<p>' + netr.string.translate('tabs.error') + '<p>');
				}
			});
		});

		firstTab.trigger('click');
	};

	$.fn.togglerTabs = function () {

		var tabs = $(this);

		// These are our tab-panels. The area that toggles
		var tabsContent = tabs.find('.tabs__tabcontent');
		tabsContent.attr('role', 'tabpanel');

		// These are the tabs that constitute the "menu"
		var tablistItems = tabs.find('.tabs__item a');
		tablistItems.attr('role', 'tab');

		// The id of the panel (if any within the tabs) referenced by the fragment identifier
		// TODO: Do i want this to happen if only the fragment identifier is changed (and id is within the tabs)
		// TODO: maybe move this to after the toggleTab functions initialisation. Might make it more readable.
		var preSelectedId = tabsContent.filter(window.location.hash).attr('id');

		// show the tab selected by the id
		if (preSelectedId !== undefined) {
			toggleTab(tablistItems.filter('[href="#' + preSelectedId + '"]'));
		}
		// show the first tab
		else {
			toggleTab(tablistItems.first());
		}

		// makes the first child of each panel focusable
		tabsContent.each(function () {
			$(this).children().first().attr('tabindex', '0');
		});

		// Changes the role from navigation to tablist, cause without script its more a navigation than a
		// tab-thingy.
		tabs.children('[role=navigation]').attr('role', null).children('ul').attr('role', 'tablist');

		// To prevent screenreaders from seeing only one tab in the group "tab 1 of 1".
		tabs.find('.tabs__item').attr('role', 'presentation');

		// Marks other tabs as not selected
		tablistItems.each(function () {
			var self = $(this);
			if (!self.parent('.tabs__item--selected').length) {
				self.attr('aria-selected', 'false');
			}

			// Set the aria controls to the href-value, without the #.
			self.attr('aria-controls', self.attr('href').slice(1));
		});

		// Toggle tabs
		function toggleTab(tabToSelect) {

			var selectedTabContent = tabsContent.filter(tabToSelect.attr('href'));

			// Hide panels
			tabsContent.not(selectedTabContent).hide().attr('aria-hidden', true);

			// show panel
			selectedTabContent.show().attr('aria-hidden', false);

			// Remove selected highlight
			$('.tabs__item--selected', tabs).removeClass('tabs__item--selected');

			// remove focusability and aria-selected
			tablistItems.not(tabToSelect).attr({
				'aria-selected': 'false',
				'tabindex': '-1'
			});

			// Add highlight and focus on selected tab (focus important for keyboard)
			tabToSelect.attr({
				'aria-selected': 'true',
				'tabindex': '0'
			}).focus();

			// add "selected class"
			tabToSelect.closest('.tabs__item').addClass('tabs__item--selected');
		}

		// Keyboard navigation
		tablistItems.on('keydown', netr.throttle(function (e) {
			// Note the function is throttled to get a reasonable
			// speed on the tab-switching while holding a key.

			// Get the position in the set of tablistItems.
			var targetIndex = tablistItems.index(e.target);
			switch (e.keyCode) {
				case 37: case 38:
					// left arrow, up arrow
					targetIndex = targetIndex - 1;
					break;
				case 39: case 40:
					// Right arrow, Down arrow
					targetIndex = targetIndex + 1;
					break;
				case 36:
					// Home
					// select first tab
					targetIndex = 0;
					break;
				case 35:
					// End
					// select last tab
					targetIndex = tablistItems.length - 1;
					break;
				default:
					targetIndex = false;
					break;
			}

			if (targetIndex !== false) {
				// make the tabs "wrap". Trust me - it works
				targetIndex = (tablistItems.length + targetIndex) % tablistItems.length;

				var tabToSelect = tablistItems.eq(targetIndex);
				toggleTab(tabToSelect);
			}
		}, 250));

		// When clicked
		tablistItems.on('click', function (e) {
			e.preventDefault();
			toggleTab($(this));
		});
	};

}(jQuery));