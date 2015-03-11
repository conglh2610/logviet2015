'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (languages, filterValue) {
            if (!filterValue) return languages;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < languages.length; i++) {
                var obj = languages[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Description.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('languageFilter', languageFilter);

});
