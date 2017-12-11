mcss.Order = {
    refill: function (seqId, callback) {
        $.post("patient/order/refill", { id: seqId }, function (data) {
            callback();
        });
    },
    undoRefill: function (seqId, callback) {
        $.post("patient/order/refill/undo", { id: seqId }, function (data) {
            callback();
        });
    },
    order: function (seqId, callback) {
        $.post("patient/order/create", { id: seqId }, function (data) {
            callback();
        });
    },
    undoOrder: function (seqId, callback) {
        $.post("patient/order/undo", { id: seqId }, function (data) {
            callback();
        });
    },
    init: function () {
        $(':checkbox.order-checkbox').click(function () {
            var elem = $(this);
            if (elem.prop('checked')) {
                mcss.Order.refill(elem.val(), function () {
                    elem.next('span').html('Påfylld').addClass('done').show();
                });
            }
            else {
                mcss.Order.undoRefill(elem.val(), function () {
                    if(elem.next('span').hasClass('ordered')) {
                        elem.next('span').removeClass('done').html("Beställd");
                    }
                    else {
                        elem.next('span').removeClass('done').html("");
                    }
                });
            }
        });

        $(':checkbox.pre-order-checkbox').click(function () {
            var elem = $(this);
            if (elem.prop('checked')) {
                mcss.Order.order(elem.val(), function () {
                    if (!elem.parent().parent().parent().find('span').hasClass('done')) {
                        elem.parent().parent().parent().find('span').addClass('ordered').html("Beställd");
                    }
                });
            }
            else {
                mcss.Order.undoOrder(elem.val(), function () {
                    elem.parent().parent().parent().find('span').removeClass('ordered');
                    if (!elem.parent().parent().parent().find('span').hasClass('done')) {
                        elem.parent().parent().parent().find('span').html("");
                    }
                });
            }
        });
    }
}