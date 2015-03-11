'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (carCategories, filterValue) {
            if (!filterValue) return carCategories;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < carCategories.length; i++) {
                var obj = carCategories[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('carCategoryFilter', carCategoryFilter);

});
