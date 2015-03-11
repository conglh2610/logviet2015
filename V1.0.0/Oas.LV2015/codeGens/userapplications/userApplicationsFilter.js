'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (userApplications, filterValue) {
            if (!filterValue) return userApplications;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < userApplications.length; i++) {
                var obj = userApplications[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.CreateBy.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('userApplicationFilter', userApplicationFilter);

});
