'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (parents, filterValue) {
            if (!filterValue) return parents;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < parents.length; i++) {
                var obj = parents[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.FirstName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.LastName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Address.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.PhoneNumber.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Email.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.FaceBook.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Twitter.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.googleplus.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('parentFilter', parentFilter);

});
