'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (studentFees, filterValue) {
            if (!filterValue) return studentFees;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < studentFees.length; i++) {
                var obj = studentFees[i];
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

    
    app.filter('studentFeeFilter', studentFeeFilter);

});
