mcss.Calendar = {
    unmarkCat: function (cat) {
        var id = cat.attr('id');
        $(".act." + id).hide();
    },
    markCat: function (cat) {
        var id = cat.attr('id');
        $(".act." + id).show();
    },
    unmarkAll: function () {
        $('#cal-all').attr('checked', false);
        $('.act').hide();
    },
    markAll: function () {
        $('.cal-filter :checkbox').attr('checked', false);
        $('#cal-all').attr('checked', true);
        $('.act').show();
    },
    quittance: function (task) {
        $.ajax({
            url: '/patient/calendar/quittance',
            dataType: 'JSON',
            method: 'get',
            data: { id: task.attr('id'), date: task.attr('name') },
            success: function (data) {
                if (data) {
                    if (data.success) {
                        task.attr('checked', true);
                        task.next(".cal-label-done").show();
                        task.next("label.cal-label").html("Kvitterad av " + data.name);
                    }
                }
            }
        });
    },
    unQuittance: function (task) {
        $.ajax({
            url: '/patient/calendar/unquittance',
            dataType: 'JSON',
            method: 'get',
            data: { id: task.attr('id'), date: task.attr('name') },
            success: function (data) {
                if (data) {
                    task.attr('checked', false);
                    task.next(".cal-label-done").hide();
                    task.next("label.cal-label").html("Ej kvitterad");
                }
            }
        });
    },
    init: function () {
        $('.cal-filter :checkbox').click(function () {
            var elem = $(this);
            if (elem.parent().hasClass('cal-all')) {
                if (elem.is(':checked')) {
                    mcss.Calendar.markAll();
                }
                else {
                    mcss.Calendar.unmarkAll();
                }
            }
            else {
                if (elem.is(':checked')) {
                    if ($('#cal-all').is(':checked')) {
                        mcss.Calendar.unmarkAll();
                    }
                    mcss.Calendar.markCat(elem);
                }
                else {
                    mcss.Calendar.unmarkCat(elem);
                }
            }
        });
        $(':checkbox.cal-quittance').click(function () {
            if ($(this).is(':checked')) {
                mcss.Calendar.quittance($(this));
            }
            else {
                mcss.Calendar.unQuittance($(this));
            }

        });
    }
};
