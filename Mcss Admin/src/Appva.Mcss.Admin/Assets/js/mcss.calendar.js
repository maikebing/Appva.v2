mcss.Calendar = {
    unmarkCat: function (cat) {
        var id = cat.attr('id');
        $(".act." + id).hide();
        $('#select-all').attr('checked', false);
    },
    markCat: function (cat) {
        var id = cat.attr('id');
        $(".act." + id).show();
    },
    unmarkAll: function () {
        $('#select-all').attr('checked', false);
        $('.act').hide();
    },
    markAll: function () {
        $('.cal-filter :checkbox').attr('checked', false);
        $('#select-all').attr('checked', true);
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
                        task.prev("label.cal-label").html("Kvitterad av " + data.name);
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
                    task.prev("label.cal-label").html("Ej kvitterad");
                }
            }
        });
    },
    focus: function (id) {
        console.log(id);
        $("." + id).parent().addClass("focus");
    },
    unFocus: function (id) {
        console.log(id);
        $("." + id).parent().removeClass("focus");
    },
    init: function () {
        $("span.act").hover(function (e){
            var id = $(this).attr('id');
            $("." + id).parent().addClass("focus");
        }, function (e) {
            var id = $(this).attr('id');
            $("." + id).parent().removeClass("focus");
        });
        $('.cal-filter :checkbox').change(function () {
            var elem = $(this);
            if (!elem.parent().hasClass('select-all')) {
                if (elem.is(':checked')) {
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
        $('.month-row').each(function (e) {
            var elem = $(this);
            elem.ready(function (e) {
                var height = elem.height();
                var gridHeight = elem.find('.evt-grid').height();
                if ((gridHeight + 25) > height) {
                    elem.height(gridHeight + 25);
                }
            });
        });

       

    }
};
