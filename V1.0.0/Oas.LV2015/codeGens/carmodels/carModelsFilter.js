'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (carModels, filterValue) {
            if (!filterValue) return carModels;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < carModels.length; i++) {
                var obj = carModels[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Image.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('carModelFilter', carModelFilter);

});
