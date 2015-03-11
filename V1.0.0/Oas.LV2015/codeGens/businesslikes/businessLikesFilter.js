'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (businessLikes, filterValue) {
            if (!filterValue) return businessLikes;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < businessLikes.length; i++) {
                var obj = businessLikes[i];
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

    
    app.filter('businessLikeFilter', businessLikeFilter);

});
