'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (rooms, filterValue) {
            if (!filterValue) return rooms;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < rooms.length; i++) {
                var obj = rooms[i];
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

    
    app.filter('roomFilter', roomFilter);

});
