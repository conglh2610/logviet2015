'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (carAccidents, filterValue) {
            if (!filterValue) return carAccidents;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < carAccidents.length; i++) {
                var obj = carAccidents[i];
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

    
    app.filter('carAccidentFilter', carAccidentFilter);

});
