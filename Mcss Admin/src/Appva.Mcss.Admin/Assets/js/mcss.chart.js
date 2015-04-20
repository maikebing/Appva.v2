mcss.chart = {
    'Load': function (opts) {
        var self = this,
            selector = opts.selector,
            url = opts.url,
            parameters = opts.parameters,
            min = opts.min,
            max = opts.max;
        $(selector).addClass('loading').append('<div class="loader">Laddar data.</div>');
        $.getJSON(url, parameters)
            .success(function (data) { self.Create(selector, data, min, max); })
            .error(function () { $(selector).addClass('error'); $(selector).empty(); $(selector).append('<div class="loader">Ett fel inträffade!</div>') })
            .complete(function () { $(selector).removeClass('loading'); });
    },
    'Create': function (selector, data, min, max) {
        var self = this;
        var options = {
            'colors': ['#445068'],
            'lines': { 'show': true, 'lineWidth': 3 },
            'points': { 'show': true, 'radius': 4, 'symbol': 'circle' },
            'grid': { 'hoverable': true, 'color': '#B6C2E0', 'backgroundColor': '#ffffff' },
            'yaxis': { 'max': 100, 'min': 0, 'color': '#445068' },
            'xaxis': { 'color': '#445068', 'mode': 'time', 'minTickSize': [1, 'day'], 'monthNames': Date.abbrMonthNames, 'min': min.getTime(), 'max': max.getTime() }
        };
        $.plot($(selector), [data], options);
        $(selector).bind('plothover', function (event, pos, item) {
            if (item) {
                if (previousPoint != item.datapoint) {
                    previousPoint = item.datapoint;
                    $('#tooltip').remove();
                    var x = item.datapoint[0], y = item.datapoint[1];
                    var d = new Date(x);
                    self.ShowToolTip(item.pageX, item.pageY, 'I tid ' + Math.round(y) + '% <br/> ' + d.asString());
                }
            } else {
                $('#tooltip').remove();
                previousPoint = null;
            }
        });
    },
    'ShowToolTip': function (x, y, contents) {
        // TODO: Move to CSS.
        $('<div id="tooltip">' + contents + '</div>').css({
            'position': 'absolute',
            'display': 'block',
            "z-index": 10,
            'top': y + 5,
            'left': x + 5,
            'border': '2px solid #702515',
            'padding': '1em',
            'background-color': '#893A29',
            'opacity': 0.85,
            'margin': '5px',
            'font-size': '10px',
            'color': '#fff',
            'font-weight': 'bold',
            'text-shadow': '1px 1px 3px #702515',
            'border-radius': '3px',
            'box-shadow': '1px 1px 3px #ababab'
        }).appendTo("body").fadeIn(125);
    }
};