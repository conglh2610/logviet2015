'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (classTeachers, filterValue) {
            if (!filterValue) return classTeachers;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < classTeachers.length; i++) {
                var obj = classTeachers[i];
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

    
    app.filter('classTeacherFilter', classTeacherFilter);

});
