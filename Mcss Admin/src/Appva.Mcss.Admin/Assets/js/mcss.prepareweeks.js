mcss.PrepareWeeks = {
    unmarkDay: function (linkelm) {
        var t = linkelm;
        t.parent().addClass('loading');
        $.ajax({
            url: t.attr('href'),
            dataType: 'JSON',
            method: 'get',
            success: function (data) {
                t.parent().removeClass('sel loading');
                var newURL = t.attr('href').replace("unMark=True", "unMark=False");
                t.attr('href', newURL);
                t.parent().find(".name").html("-");
            }
        });
    },
    markDay: function (linkelm) {
        var t = linkelm;
        t.parent().addClass('loading');
        $.ajax({
            url: t.attr('href'),
            dataType: 'JSON',
            method: 'get',
            success: function (data) {
                t.parent().removeClass('loading').addClass('sel');
                var newURL = t.attr('href').replace("unMark=False", "unMark=True");
                t.attr('href', newURL);
                t.parent().find(".name").html(data);
            }
        });
    },
    init: function () {
        var elm = $('#prepare-weeks');
        if (elm.size()) {
            $('a.mark').click(function (e) {
                var t = $(this);
                if (!t.parent().hasClass('loading')) {
                    if (t.parent().hasClass('sel')) {
                        mcss.PrepareWeeks.unmarkDay(t);
                    } else {
                        mcss.PrepareWeeks.markDay(t);
                    }
                }
                e.stopPropagation();
                e.preventDefault();
            });
        }
    }
};