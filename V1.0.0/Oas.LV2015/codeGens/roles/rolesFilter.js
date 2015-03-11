'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (roles, filterValue) {
            if (!filterValue) return roles;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < roles.length; i++) {
                var obj = roles[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Description.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Discriminator.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('roleFilter', roleFilter);

});
