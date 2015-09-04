'use strict';
(function ($) {
    $.fn.PasswordStrength = function (opts) {
        var options = $.extend({ text: 'L&ouml;senordets styrka:', scores: ['mycket l&aring;g', 'l&aring;g', 'medel', 'h&ouml;g', 'mycket h&ouml;g'], classes: ['very-weak', 'weak', 'medium', 'strong', 'very-strong'] }, opts);
        return this.each(function () {
            var el = $(this), visible = false;
            el.after('<div class="password-strength"><div data-meter="' + el.attr('id') + '" class="very-weak"><p>' + options.text + ' <span></span></p><div class="meter"><div class="strength"></div></div></div></div>');
            var meter = $('div[data-meter="' + $(this).attr('id') + '"]');
            el.bind('keyup', function () {
                var val = $(this).val();
                if (val !== 'undefined' && val !== '') {
                    if (!visible) {
                        meter.parent().fadeIn();
                    }
                    var result = zxcvbn(val);
                    meter.removeClass().addClass(options.classes[result.score]);
                    meter.find('p span').html(options.scores[result.score]);
                } else {
                    meter.parent().fadeOut();
                    visible = false;
                }
            });
        });
    };
}(jQuery));