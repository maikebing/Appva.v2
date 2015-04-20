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
        if (content.hasClass('lb-validate-form')) this.applyValidation(content.data('valclass'), content.data('valparams'));
        mcss.customSelect($('.activity-edit .col:first'));

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