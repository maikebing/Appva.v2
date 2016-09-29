var mcss = mcss || {};
mcss.auth =
{
    _expiration: null,
    _timer:      null,
    init: function(milliseconds, options)
    {
        var self = this;
        self.options = $.extend({
            showCountdown:             true,
            showCountdownAfterMinutes: 5,
            showCountdownIsVisible:    false,
            redirectToLogin:           true,
            loginUrl:                  null
        }, options);
        self._second = 1000;
        self._minute = self._second * 60;
        self._hour   = self._minute * 60;
        self._day    = self._hour * 24;
        self._expiration = new Date(milliseconds);
        if (self.options.showCountdown)
        {
            $('body').append('<div id="message-container"><div class="automatic-logout-alert alert alert-success"><a class="close" data-dismiss="alert" href="#">&times;</a><div class="title">Information</div><div class="body"></div></div></div>');
            var message = $('#message-container .automatic-logout-alert').first();
            message.find('.close').click(function (evt)
            {
                self.options.showCountdown = false;
                self.options.showCountdownIsVisible = false; 
                message.fadeOut('slow');
                evt.preventDefault();
            });
        }
        self._timer = setInterval(function()
        {
            var now = new Date();
            if (now > self._expiration) 
            {
                if (self.options.redirectToLogin)
                {
                    window.location = self.options.loginUrl + '?auto=true';
                }
                else
                {
                    self.options.showCountdown = false;
                    self.options.showCountdownIsVisible = false;
                    message.fadeOut('slow');
                    clearInterval(self._timer);
                    $.ajax({
                        url: self.options.loginUrl + '?returnUrl=' + window.location.pathname,
                        dataType: 'html',
                        method:   'get',
                        success: function (data) {
                            var content = $(data);
                            var wrap = $('<div class="lb-wrap" />');
                            var panel = $('<div class="login lb-panel lb-panel-small" />');
                            var blackout = $('<div class="lb-blackout" />').hide();
                            $('body').append(blackout);
                            $('body').append(wrap);
                            wrap.append(panel);
                            panel.append(content);
                            blackout.fadeTo(100, 0.7);
                        }
                    });
                }
            }
            else
            {
                if (self.options.showCountdown)
                {
                    //// copy current date time and set the count down to show X minutes before the cookie expires.
                    var future = new Date(now.getTime());
                    future.setMinutes(future.getMinutes() + self.options.showCountdownAfterMinutes);
                    if (future > self._expiration)
                    {
                        var distance = self._expiration - now;
                        if (distance > 0)
                        {
                            var minutes = Math.floor((distance % self._hour)   / self._minute);
                            var seconds = Math.floor((distance % self._minute) / self._second);
                            if (!self.options.showCountdownIsVisible)
                            {
                                message.fadeIn('slow');
                                self.options.showCountdownIsVisible = true;
                            }
                            message.find('.body').text('Du loggas automatiskt ut om ' + minutes + ' minuter ' + seconds + ' sekunder');
                        }
                    }
                }
            }
        }, 1000);
    },
    setExpiration: function(milliseconds)
    {
        this._expiration = new Date(milliseconds);
    }
};