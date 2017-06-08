$(function () {
    mcss.lightbox.init();
    mcss.PrepareWeeks.init();
    mcss.Calendar.init();
   

    // Handle deletion of lists
    $('.signlist-collection a.item-delete').click(function () {
        var clicked = $(this);
        var listtitle = clicked.prev().text();
        var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
        content.find('.prompt').append('<p class="warning">Vill du verkligen ta bort ' + listtitle + '?</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">Ta bort listan</a><a class="cancel" href="#">Avbryt</a>');
        mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
            $('.lb-panel a.cancel').click(function () {
                $('.lb-blackout, .lb-wrap').remove();
                return false;
            });
        });
        return false;
    });

    $('#signlist a.delete').click(function () {
        var clicked = $(this);
        var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
        content.find('.prompt').append('<p class="warning">Vill du verkligen ta bort raden?</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">Ta bort raden</a><a class="cancel" href="#">Avbryt</a>');
        mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
            $('.lb-panel a.cancel').click(function () {
                $('.lb-blackout, .lb-wrap').remove();
                return false;
            });
        });
        return false;
    });

    $('#signlist .btn-del').click(function () {
        var clicked = $(this);
        var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
        content.find('.prompt').append('<p class="warning">Vill du verkligen ta bort signeringen?</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">Ta bort raden</a><a class="cancel" href="#">Avbryt</a>');
        mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
            $('.lb-panel a.cancel').click(function () {
                $('.lb-blackout, .lb-wrap').remove();
                return false;
            });
        });
        return false;
    });

    $('#signlist .action-denied').click(function () {
        var clicked = $(this);
        var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
        content.find('.prompt').append('<p class="warning">Signeringen kan inte tas bort eftersom den anv&auml;nds av en eller flera listor.</p>');
        content.find('.prompt').append('<a class="cancel" href="#">St&auml;ng</a>');
        mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
            $('.lb-panel a.cancel').click(function () {
                $('.lb-blackout, .lb-wrap').remove();
                return false;
            });
        });
        return false;
    });

    $('.tests .btn-del').click(function () {
        var clicked = $(this);
        var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
        content.find('.prompt').append('<p class="warning">Vill du verkligen ta bort kunskapstestet?</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">Ta bort raden</a><a class="cancel" href="#">Avbryt</a>');
        mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
            $('.lb-panel a.cancel').click(function () {
                $('.lb-blackout, .lb-wrap').remove();
                return false;
            });
        });
        return false;
    });

    $('.people .btn-del, .relatives .btn-del').click(function () {
        var clicked = $(this);
        var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
        content.find('.prompt').append('<p class="warning">Vill du verkligen ta bort personen?</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">Ta bort personen</a><a class="cancel" href="#">Avbryt</a>');
        mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
            $('.lb-panel a.cancel').click(function () {
                $('.lb-blackout, .lb-wrap').remove();
                return false;
            });
        });
        return false;
    });
});