'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (membershipPackages, filterValue) {
            if (!filterValue) return membershipPackages;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < membershipPackages.length; i++) {
                var obj = membershipPackages[i];
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

    
    app.filter('membershipPackageFilter', membershipPackageFilter);

});
