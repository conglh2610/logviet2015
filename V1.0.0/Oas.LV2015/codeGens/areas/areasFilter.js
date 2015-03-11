'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (areas, filterValue) {
            if (!filterValue) return areas;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < areas.length; i++) {
                var obj = areas[i];
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

    
    app.filter('areaFilter', areaFilter);

});
