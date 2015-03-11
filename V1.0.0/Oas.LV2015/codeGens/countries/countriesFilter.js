'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (countries, filterValue) {
            if (!filterValue) return countries;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < countries.length; i++) {
                var obj = countries[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('countryFilter', countryFilter);

});
