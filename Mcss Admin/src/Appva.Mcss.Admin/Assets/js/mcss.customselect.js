mcss.customSelect = function(el, options) {
    var options = $.extend({}, {
        spanClass: 'span.custom-select', // Class name for custom styled span
        selectClass: 'select.custom-select', // Class name for select to be replaced
        currentClass: 'current', // Class name hover/focus effect on span
        autoSubmit: false
    }, options || {});
    var addFn = function(){$(this).next(options.spanClass).addClass(options.currentClass);};
    var removeFn = function(){$(this).next(options.spanClass).removeClass(options.currentClass);};

    if(options.autoSubmit) {
        el.find('.submit-area').hide();
    }
    if (!$.browser.opera) {
        el.find('select').addClass('custom-select');
        el.find(options.selectClass).each(function(){
            $(this).wrap('<div class="custom-select-wrap" />');
            var that = $(this).get(0);
            var title = $('option:first',this).text();
            if($('option:selected', this).val() != ''  ) title = $('option:selected',this).text();
            that.onchange = function() {
                val = $('option:selected', this).text();
                $(this).next().text(val);
                if(options.autoSubmit) {
                    that.form.submit();
                }
            };
            $(this)
                .css({'z-index':10,'opacity':0,'-khtml-appearance':'none'})
                .after('<span class="custom-select">' + title + '</span>')
                .hover(addFn,removeFn)
                .focus(addFn)
                .blur(removeFn);
            
            if ($.browser.msie) $(this).css({'width':'auto'});
        });
    };
};

$(function() {
    mcss.customSelect($('#subheader .filter'));
    mcss.customSelect($('.filter-ctrl'));
    mcss.customSelect($('.global-filter'));
});