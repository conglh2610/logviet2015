'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (businessPromotions, filterValue) {
            if (!filterValue) return businessPromotions;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < businessPromotions.length; i++) {
                var obj = businessPromotions[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Description.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Title.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('businessPromotionFilter', businessPromotionFilter);

});
