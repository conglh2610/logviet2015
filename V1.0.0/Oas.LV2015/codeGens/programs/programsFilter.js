'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (programs, filterValue) {
            if (!filterValue) return programs;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < programs.length; i++) {
                var obj = programs[i];
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

    
    app.filter('programFilter', programFilter);

});
