'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (emailTemplates, filterValue) {
            if (!filterValue) return emailTemplates;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < emailTemplates.length; i++) {
                var obj = emailTemplates[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Name.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Content.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('emailTemplateFilter', emailTemplateFilter);

});
