'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (drivers, filterValue) {
            if (!filterValue) return drivers;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < drivers.length; i++) {
                var obj = drivers[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Address.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Phone.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.DriverLevel.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.DriverLicense.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('driverFilter', driverFilter);

});
