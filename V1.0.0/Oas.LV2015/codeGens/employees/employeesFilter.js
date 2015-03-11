'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (employees, filterValue) {
            if (!filterValue) return employees;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < employees.length; i++) {
                var obj = employees[i];
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

    
    app.filter('employeeFilter', employeeFilter);

});
