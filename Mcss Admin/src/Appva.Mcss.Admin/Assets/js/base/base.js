/**
 * This is the main project file. This is where all other scripts should be initiated,
 * through the "newcontent" event.
 */

$(function () {
	'use strict';

	// Trigger event when new content is loaded.
	$('body').trigger('newcontent');
});
