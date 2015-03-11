'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (bookingNotes, filterValue) {
            if (!filterValue) return bookingNotes;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < bookingNotes.length; i++) {
                var obj = bookingNotes[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Note.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.CreatedBy.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('bookingNoteFilter', bookingNoteFilter);

});
