'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (results, filterValue) {
            if (!filterValue) return results;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < results.length; i++) {
                var obj = results[i];
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

    
    app.filter('resultFilter', resultFilter);

});
