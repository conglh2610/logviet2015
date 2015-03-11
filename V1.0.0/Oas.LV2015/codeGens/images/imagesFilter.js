'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (images, filterValue) {
            if (!filterValue) return images;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < images.length; i++) {
                var obj = images[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Caption.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Url.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('imageFilter', imageFilter);

});
