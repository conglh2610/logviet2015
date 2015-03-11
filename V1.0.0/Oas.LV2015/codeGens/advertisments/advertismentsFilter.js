'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (advertisments, filterValue) {
            if (!filterValue) return advertisments;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < advertisments.length; i++) {
                var obj = advertisments[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.CustomerName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.ImageUrl.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Url.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Description.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('advertismentFilter', advertismentFilter);

});
