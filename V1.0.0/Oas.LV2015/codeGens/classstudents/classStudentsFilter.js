'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (classStudents, filterValue) {
            if (!filterValue) return classStudents;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < classStudents.length; i++) {
                var obj = classStudents[i];
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

    
    app.filter('classStudentFilter', classStudentFilter);

});
