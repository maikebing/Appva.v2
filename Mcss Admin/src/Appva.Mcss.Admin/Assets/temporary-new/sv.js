/* globals define, module, require */
var netr = window['netr'] || {};

netr.string.addTranslations('sv', {
	datepicker: {
		selectDate: 'Välj datum'
	},
	dialog: {
		close: 'Stäng',
		errorMessage: 'Ett fel har tyvärr uppstått. Var vänlig försök igen.'
	},
	exitConfirmation: {
		leaveForm: 'Lämna formulär',
		message: 'Du har inte sparat informationen i formuläret. Vill du verkligen lämna sidan?',
		cancel: 'Avbryt',
		leavePage: 'Lämna sida'
	},
	inputValueList: {
		empty: 'Ännu inget värde tillagt'
	},
	sortableTables: {
		sortedBy: 'Sorterat efter',
		ascending: 'stigande',
		descending: 'fallande'
	},
	spinner: {
		increase: 'Öka',
		decrease: 'Minska'
	},
	tabs: {
		error: 'Kan inte ladda innehåll. Försök igen'
	},
	updateSection: {
		error: {
			formHeading: 'Formuläret kunde inte skickas in',
			linkHeading: 'Innehållet kunde inte hämtas',
			instruction: 'Eventuellt kan det hjälpa att testa igen.',
			errorCode: 'Felkod',
			errorMessage: 'Felmeddelande'
		}
	},
	validate: {
		valid: 'Godkänt värde'
	},
});

(function (factory) {
	'use strict';
	if (typeof define === 'function' && define.amd) {
		define(['jquery', '../jquery.validate'], factory);
	}
	else if (typeof module === 'object' && module.exports) {
		module.exports = factory(require('jquery'));
	}
	else {
		factory(jQuery);
	}
}(function ($) {
	'use strict';

/*
 * Translated messages extensions of the jQuery validation plugin.
 * Locale: SV (Swedish; Svenska)
 */
	$.extend($.validator.messages, {
		personnumber: 'Ange ett giltligt personnummer.',
		step: 'Talet måste vara en multipel av {0}',
		greaterThan: 'Värdet måste vara större än {0}',
		restrictedDate: 'Datumet går ej att välja',
		datelimit: function (argument) {
			return {
				'future': 'Välj ett framtida datum (eller idag)',
				'past': 'Välj ett passerat datum (eller idag)'
			}[argument];
		}
	});

}));