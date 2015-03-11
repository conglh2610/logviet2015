'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (userRoles, filterValue) {
            if (!filterValue) return userRoles;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < userRoles.length; i++) {
                var obj = userRoles[i];
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

    
    app.filter('userRoleFilter', userRoleFilter);

});
