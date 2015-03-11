'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (schedulers, filterValue) {
            if (!filterValue) return schedulers;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < schedulers.length; i++) {
                var obj = schedulers[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('schedulerFilter', schedulerFilter);

});
