'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (messageHistories, filterValue) {
            if (!filterValue) return messageHistories;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < messageHistories.length; i++) {
                var obj = messageHistories[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Message.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('messageHistoryFilter', messageHistoryFilter);

});
