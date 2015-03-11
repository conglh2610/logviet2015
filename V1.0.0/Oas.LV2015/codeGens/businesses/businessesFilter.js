'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (businesses, filterValue) {
            if (!filterValue) return businesses;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < businesses.length; i++) {
                var obj = businesses[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Zipcode.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Address.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Phone.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Email.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Information.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.SortDescription.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Facebook.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Twitter.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.CreateBy.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('businessFilter', businessFilter);

});
