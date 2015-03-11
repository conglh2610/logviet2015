'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (businessComments, filterValue) {
            if (!filterValue) return businessComments;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < businessComments.length; i++) {
                var obj = businessComments[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Comment.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.CreateDate.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('businessCommentFilter', businessCommentFilter);

});
