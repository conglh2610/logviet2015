'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (businessCategories, filterValue) {
            if (!filterValue) return businessCategories;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < businessCategories.length; i++) {
                var obj = businessCategories[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.GooglePlaceIconUrl.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('businessCategoryFilter', businessCategoryFilter);

});
