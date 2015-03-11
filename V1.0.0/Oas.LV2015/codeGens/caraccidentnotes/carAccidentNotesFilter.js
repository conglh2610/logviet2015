'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (carAccidentNotes, filterValue) {
            if (!filterValue) return carAccidentNotes;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < carAccidentNotes.length; i++) {
                var obj = carAccidentNotes[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Title.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Description.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('carAccidentNoteFilter', carAccidentNoteFilter);

});
