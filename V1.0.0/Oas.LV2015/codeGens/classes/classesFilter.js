'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (classes, filterValue) {
            if (!filterValue) return classes;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < classes.length; i++) {
                var obj = classes[i];
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

    
    app.filter('classFilter', classFilter);

});
