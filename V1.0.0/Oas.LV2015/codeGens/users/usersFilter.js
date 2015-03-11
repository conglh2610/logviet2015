'use strict';

define(['app'], function (app) {

    var roomFilter = function () {

        return function (users, filterValue) {
            if (!filterValue) return users;

            var matches = [];
            filterValue = filterValue.toLowerCase();
            for (var i = 0; i < users.length; i++) {
                var obj = users[i];
                if(//obj.Name.toLowerCase().indexOf(filterValue) > -1 ||
					/*[@Conditions]*/
				(obj.Email.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.PasswordHash.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.SecurityStamp.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.PhoneNumber.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.UserName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.FirstName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.LastName.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Address.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Phone.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.ProfileImage.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Tips.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.ContactTitle.toLowerCase().indexOf(filterValue) > -1)
				|| (obj.Discriminator.toLowerCase().indexOf(filterValue) > -1)

				)
				{
                    matches.push(obj);
                }
            }
            return matches;
        };
    };

    
    app.filter('userFilter', userFilter);

});
