'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (skills, filterValue) {
            if (!filterValue) return skills;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < skills.length; i++) {
                var obj = skills[i];
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

    
    app.filter('skillFilter', skillFilter);

});
