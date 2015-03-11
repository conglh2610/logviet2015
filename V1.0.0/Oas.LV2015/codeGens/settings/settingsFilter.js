'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (settings, filterValue) {
            if (!filterValue) return settings;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < settings.length; i++) {
                var obj = settings[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.DefaultGLng.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.DefaultGLa.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('settingFilter', settingFilter);

});
