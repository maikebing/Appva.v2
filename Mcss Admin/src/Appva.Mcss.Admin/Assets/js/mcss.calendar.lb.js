SetSummary = function (interval, factor, date) {
    var weekday = new Array(7);
    weekday[0] = "söndag";
    weekday[1] = "måndag";
    weekday[2] = "tisdag";
    weekday[3] = "onsdag";
    weekday[4] = "torsdag";
    weekday[5] = "fredag";
    weekday[6] = "lördag";

    var startDate = $('#StartDate').val().split("-");
    var d = new Date(startDate[0], startDate[1] - 1, startDate[2]);

    if (interval == 7) {
        $('#dateSummary').html(weekday[d.getDay()]);
    }
    else {
        if (date == "True") {
            $('#dateSummary').html("den " + d.getDate());
        }
        else {
            var weekInMonth = ((d.getDate() - (d.getDate() % 7)) / 7) + 1;
            $('#dateSummary').html("den" + " " + weekInMonth + " " + weekday[d.getDay()] + "en");
        }
    }


    var intervalDesc = "månad";
    if (interval == 7) {
        intervalDesc = "vecka";
    }

    if (factor == 1) {
        $('#intervalSummary').html("varje " + intervalDesc);
    }
    else {
        $('#intervalSummary').html("var " + factor + " " + intervalDesc);
    }
}

