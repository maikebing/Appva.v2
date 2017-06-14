$(function () {
    mcss.lightbox.init();
    mcss.PrepareWeeks.init();
    mcss.Calendar.init();
   

    // Handle deletion of lists
    $('.signlist-collection a.item-delete').click(function () {
        var clicked = $(this);
        var listtitle = clicked.prev().text();
        var content = $('<div class="lb-panel lb-panel-small"><div class="std-panel"><div class="prompt"></div></div></div>');
        content.find('.prompt').append('<p class="warning">' + language.general.removeListPrompt + ' ' + listtitle + '?</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">' + language.general.removeList + '</a><a class="cancel" href="#">' + language.general.cancelLabel + '</a>');
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
        content.find('.prompt').append('<p class="warning">' + language.general.removeRowPrompt + '</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">' + language.general.removeRow + '</a><a class="cancel" href="#">' + language.general.cancelLabel + '</a>');
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
        content.find('.prompt').append('<p class="warning">' + language.general.removeKnowledgeTest + '</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">T' + language.general.removeRow + '</a><a class="cancel" href="#">' + language.general.cancelLabel + '</a>');
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
        content.find('.prompt').append('<p class="warning">' + language.general.removeUserPrompt + '</p>');
        content.find('.prompt').append('<a class="btn btn-del" href="' + clicked.attr('href') + '">' + language.general.removeUser + '</a><a class="cancel" href="#">' + language.general.cancelLabel + '</a>');
        mcss.lightbox.openBox(content, clicked, 'lb-warning', function (content) {
            $('.lb-panel a.cancel').click(function () {
                $('.lb-blackout, .lb-wrap').remove();
                return false;
            });
        });
        return false;
    });
});