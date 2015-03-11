'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (carBookings, filterValue) {
            if (!filterValue) return carBookings;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < carBookings.length; i++) {
                var obj = carBookings[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Schduling.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('carBookingFilter', carBookingFilter);

});
