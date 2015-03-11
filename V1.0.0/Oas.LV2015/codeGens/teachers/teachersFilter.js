'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (teachers, filterValue) {
            if (!filterValue) return teachers;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < teachers.length; i++) {
                var obj = teachers[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.FirstName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.LastName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Address.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.PhoneNumber.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Email.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('teacherFilter', teacherFilter);

});
