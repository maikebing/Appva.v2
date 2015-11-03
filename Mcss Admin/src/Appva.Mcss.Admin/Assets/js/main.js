// Most Likely Not Used. Remove!
function redirect() {
    var url = window.location.href;
    window.location.href = url;
}
$(document).ready(function () {
    $('.not-implemented').click(function (evt) {
        evt.preventDefault();
        alert('Detta saknas just tillfälligt. Skriv inte upp i buggrapporten då vi vet vad som skall justeras.');
    });
    $('.delegate-view #subheader a.printp').click(function () {
        window.open($(this).attr('href'), 'mcssprint', 'width=600,height=700,toolbar=no');
        return false;
    });
    $('.alarm-list .view .alarm-view').hide();
    $('.alarm-list .btn').toggle(function () {
        $(this).html('Dölj');
        $(this).parent().parent().next().find('.alarm-view').slideDown(100);
    }, function () {
        $(this).html('Visa');
        $(this).parent().parent().next().find('.alarm-view').slideUp(100);
    });
    /*$('.delegate-view #signed-events a.btn-del').click(function () {
        $('#deleg-del').show();
        return false;
    });*/
    $('#dialognote form').submit(function () {
        if ($('#notifytime').val() != '') {
            $('.addrow:visible .notify').append('<p><strong>Notifiering</strong> ' + $('#notifytime').val() + ' minuter före slut. </p>');
            $('.addrow:visible .notify p').append($('<a />').attr('href', '#').html('Ändra').click(function () {
                $('#dialognote').show();
                return false;
            }));
            $('.addrow:visible .btn').hide();
        }
        $('#dialognote').hide();
        return false;
    });
    $('#signed-events .nav a').each(function () {
        if (!$(this).parent().hasClass('sel')) $('#' + $(this).attr('href').split('#')[1]).hide();
        $(this).click(function () {
            if (!$(this).parent().hasClass('sel')) {
                $(this).parent().parent().find('.sel').removeClass('sel');
                $('#signed-events .tabtarget').hide();
                $('#' + $(this).attr('href').split('#')[1]).show();
                $(this).parent().addClass('sel');
            }
            return false;
        });
    });

    $('a.alarm-stop').click(function () {
        alert('Kontrollera om avvikelsen ska journalföras.');
    });
    $('a.alarm-stop-all').click(function (e) {
        if (confirm('Alla avvikelser för patienten kommer att kvitteras. Kontrollera om någon avvikelse ska journalföras.')) {
            
        }
        else {
            e.stopPropagation();
            e.preventDefault();
            return false;
        }
    });

    $('#calendar a.del').click(function () {
        var conftext = 'Vill du fortsätta ta bort aktiviteten?';
        if ($(this).parent().hasClass('pause'))
            conftext = 'Pausade ordinationer kommer att återupptas. Vill du fortsätta ta bort frånvaron?';
        if (confirm(conftext)) {
            $(this).parent().slideUp(100);
        } else {
            return false;
        }

    });
    $('#signlist .days a.del').live('click', function () {
        $(this).parent().hide(150);
        return false;
    });

    $('#addeleg .deleglist li, #addeleg .newgroup').hide();
    $('#addeleg .catlist a').click(function () {
        var t = $(this);
        $('#addeleg .deleglist li').hide();
        t.parent().parent().find('.sel').removeClass('sel');
        t.parent().addClass('sel');
        $('#addeleg .deleglist .' + t.attr('class')).show();
        return false;
    });
    $('.delegdialog .multisel select').change(function () {
        if ($(this).val() != '') {
            if ($('.patients').val() != $(this).val()) {
                $(this).parent().append('<span class="person">' + $(this).find(':selected').text() + ' <a href="#" title="Ta bort">Ta bort</a><input class="patients" type="hidden" name="patients" value="' + $(this).find(':selected').val() + '"/></span>');
            }
        }
    });
    $('.delegdialog .multisel .person a').live('click', function () {
        $(this).parent().slideUp(100).remove();
        return false;
    });
    $('#addeleg #btn-newdeleg').click(function () {
        $('#addeleg .selgroup').hide();
        $('#addeleg .newgroup').show();
        return false;
    });
    $('#addeleg .newgroup a.back').click(function () {
        $('#addeleg .selgroup').show();
        $('#addeleg .newgroup').hide();
        return false;
    });
    $('#addeleg .catselect .new').hide();
    $('#addeleg .catselect select').change(function () {
        if ($(this).val() == 'new') {
            $('#addeleg .catselect .new').slideDown(100);
            $('#addeleg .catselect .new input').focus();
        } else {
            $('#addeleg .catselect .new').slideUp(100);
        }
    });

    /* NEW STUFF SEPT 2012 */

    // Radio toggles
    $('.std-form .toggle-group .radio .toggle').each(function () {
        var t = $(this);
        if (!t.attr('checked')) {
            $('.' + t.data('toggletarget')).hide();
        } else {
            t.parent().addClass('toggled');
        }
        t.click(function () {
            var t = $(this);
            t.parents('.std-form:first').find('.toggled').removeClass('toggled');
            t.parent().addClass('toggled');
            t.parents('.std-form').find('.toggle-target').hide();
            $('.' + t.data('toggletarget')).show();
        });
    });

    // Checkbox toggles
    $('.std-form .checkbox .toggle').each(function () {
        var t = $(this);
        if (!t.attr('checked')) {
            $('.' + t.data('toggletarget')).hide();
        } else {
            t.parent().addClass('toggled');
        }
        t.click(function () {
            var t = $(this);
            $('.' + t.data('toggletarget')).slideToggle(100);
        });
    });


    $('.activity-edit .freq .btn-mdatepick').each(function () {
        if ($(this).prev().val() != 'other') $(this).hide();
    });
    $('.activity-edit .freq-select').each(function () {
        var t = $(this);
        if (t.val() == 'other') {
            t.parents('.std-form:first').find('.freq-hide').hide();
            t.parents('.std-form:first').find('.freq-show').show();
        } else {
            t.parents('.std-form:first').find('.freq-hide').show();
            t.parents('.std-form:first').find('.freq-show').hide();
        }
        t.change(function () {
            var t = $(this);
            if ($(this).val() == 'other') {
                t.parents('.std-form:first').find('.freq-hide').hide();
                t.parents('.std-form:first').find('.freq-show').show();
            } else {
                t.parents('.std-form:first').find('.freq-hide').show();
                t.parents('.std-form:first').find('.freq-show').hide();
            }
        });
    });

    // Disable/enable delegation on update/create ordination
    $('.checkbox .nurse').change(function () {
        var t = $(this);
        if (t.is(':checked')) {
            $('.delegation').attr('disabled', 'disabled');
        } else {
            $('.delegation').removeAttr('disabled');
        }
    });
    
    // About screen
    $('.about-link').click(function(e) {
        e.stopPropagation();
        e.preventDefault();
        var content = $('<div class="lb-panel"></div>');
        content.append($('#footer .about').clone());
        var clicked = $(this);
        mcss.lightbox.openBox(content, clicked, 'lb-warning');
    });
    
});