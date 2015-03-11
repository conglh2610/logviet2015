'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (carItems, filterValue) {
            if (!filterValue) return carItems;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < carItems.length; i++) {
                var obj = carItems[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Description.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.CarNumber.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('carItemFilter', carItemFilter);

});
