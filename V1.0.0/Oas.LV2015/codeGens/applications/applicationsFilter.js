'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (applications, filterValue) {
            if (!filterValue) return applications;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < applications.length; i++) {
                var obj = applications[i];
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

    
    app.filter('applicationFilter', applicationFilter);

});
