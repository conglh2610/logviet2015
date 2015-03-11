'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (packageItems, filterValue) {
            if (!filterValue) return packageItems;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < packageItems.length; i++) {
                var obj = packageItems[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Description.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('packageItemFilter', packageItemFilter);

});
