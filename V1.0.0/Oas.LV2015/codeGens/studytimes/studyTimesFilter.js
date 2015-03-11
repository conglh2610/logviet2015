'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (studyTimes, filterValue) {
            if (!filterValue) return studyTimes;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < studyTimes.length; i++) {
                var obj = studyTimes[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('studyTimeFilter', studyTimeFilter);

});
