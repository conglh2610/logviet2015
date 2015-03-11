'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (cars, filterValue) {
            if (!filterValue) return cars;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < cars.length; i++) {
                var obj = cars[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Year.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Description.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('carFilter', carFilter);

});
