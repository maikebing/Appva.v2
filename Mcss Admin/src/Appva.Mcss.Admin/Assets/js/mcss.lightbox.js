mcss.lightbox = {
    refocus: null,
    openBox: function (content, trigger, addClass, callback) {
        this.callback = !callback ? function () { return; } : callback;
        if (addClass)
            addClass = " " + addClass;
        else
            addClass = "";

        $('.lb-wrap, .lb-blackout').remove();

        if (trigger) this.refocus = trigger;

        $('body').keydown(function (e) {
            switch (e.keyCode) {
                case 27: // Esc key
                    mcss.lightbox.closeAll();
                    $('body').unbind('keydown');
                    event.preventDefault();
                    break;
            }
        });

        var panel = $('<div class="lb-wrap' + addClass + '" />');
        var blackout = $('<div class="lb-blackout" />').hide();
        content.addClass('lb-panel').show().prepend($('<a class="btn-close" href="#">Stäng</a>').click(function (e) {
            mcss.lightbox.closeAll();
            e.preventDefault();
            e.stopPropagation();
        })).css('top', $(document).scrollTop() + 100);
        $('body').append(blackout);
        $('body').append(panel);
        panel.append(content);
        blackout.fadeTo(200, 0.3);
        if (panel.find('.focusme').size()) {
            panel.find('.focusme:first').focus().select();
        } else {
            panel.find('.btn-close').focus();
        }
        if (content.hasClass('lb-validate-form')) {
            this.applyValidation(content.data('valclass'), content.data('valparams'));
        }

        $('a.lb-link').click(function (e) {
            var clicked = $(this);
            var actionUri = clicked.attr('href');
            $.ajax({
                url: actionUri,
                dataType: 'html',
                method: 'get',
                success: function (data) {
                    var content = $(data);
                    content.addClass('lb-panel');
                    mcss.lightbox.replaceContent(panel, content);
                    trigger.trigger("lightboxopen");
                }
            });
            e.preventDefault();
            e.stopPropagation();

        });

        $('.calendar-details .btn-del').click(function () {
            var clicked = $(this);
            var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
            content.find('.prompt').append('<p class="warning">Vill du verkligen ta bort den här aktiviteten?</p>');
            content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">Ta bort aktivitet</a><a class="cancel" href="#">Avbryt</a>');
            mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
                $('.lb-panel a.cancel').click(function () {
                    $('.lb-blackout, .lb-wrap').remove();
                    return false;
                });
            });
            return false;
        });

        this.callback();
    },
    applyValidation: function (dataclass, dataparams) {
        var valclass = dataclass,
			valparams = [],
			param = [];
        if (typeof dataparams != 'undefined') {
            dataparams = dataparams.split(';');
            for (var i = 0; i < dataparams.length; i++) {
                param = dataparams[i].split('=');
                if (param.length == 2) valparams[param[0]] = param[1];
            }
        }
        if (typeof valclass != 'undefined') mcss.validation.applyRules(valclass, valparams);
    },
    closeAll: function () {
        $('.lb-wrap').remove();
        $('.lb-blackout').fadeOut(200, function () { $(this).remove(); });
        if (this.refocus) this.refocus.focus();
    },
    replaceContent: function (panel, content) {
        panel.html("");
        content.addClass('lb-panel').show().prepend($('<a class="btn-close" href="#">Stäng</a>').click(function (e) {
            mcss.lightbox.closeAll();
            e.preventDefault();
            e.stopPropagation();
        })).css('top', $(document).scrollTop() + 100);
        panel.append(content);
        if (panel.find('.focusme').size()) {
            panel.find('.focusme:first').focus().select();
        } else {
            panel.find('.btn-close').focus();
        }
        if (content.hasClass('lb-validate-form')) this.applyValidation(content.data('valclass'), content.data('valparams'));
        mcss.customSelect($('.activity-edit .col:first'));
    },
    init: function () {
        $('a.lb-link').click(function (e) {
            var clicked = $(this);
            var actionUri = clicked.attr('href');
            $.ajax({
                url: actionUri,
                dataType: 'html',
                method: 'get',
                success: function (data) {
                    var content = $(data);
                    content.addClass('lb-panel');
                    mcss.lightbox.openBox(content, clicked, 'std-lb');
                    clicked.trigger("lightboxopen");
                }
            });
            e.preventDefault();
            e.stopPropagation();

        });
    }
};