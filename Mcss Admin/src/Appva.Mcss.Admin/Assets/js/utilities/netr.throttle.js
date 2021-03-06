(function () {
	'use strict';
	/**
	Throttles a function call. Great for window resize, keydowns and mouse move.
	Usage example:
	$('body').on('mousemove', throttle(function (event) {
		console.log('tick');
	}, 1000));
	*/

	netr.throttle = function (fn, threshhold, scope) {
		threshhold || (threshhold = 250);
		var last;
		var deferTimer;
		return function () {
			var context = scope || this;

			var now = +new Date;
			var args = arguments;

			if (last && now < last + threshhold) {
				// hold on to it
				clearTimeout(deferTimer);
				deferTimer = setTimeout(function () {
					last = now;
					fn.apply(context, args);
				}, threshhold);
			}
			else {
				last = now;
				fn.apply(context, args);
			}
		};
	};
}());