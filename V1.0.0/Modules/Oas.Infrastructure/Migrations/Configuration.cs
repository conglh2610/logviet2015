namespace Oas.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Oas.Infrastructure.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<Oas.Infrastructure.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Oas.Infrastructure.DatabaseContext context)
        {
            // This method will be called after migrating to the latest version.
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<Role>(new RoleStore<Role>(context));

            #region Migrate User and Roles
            if (!roleManager.RoleExists(UserRoles.Administrator)) context.Roles.AddOrUpdate(new Role()
            {
                Name = UserRoles.Administrator,
                Description = UserRoles.Administrator,
                ManageBusiness = true,
                ManageMembershipPackage = true,
                ManageUser = true

            });

            if (!roleManager.RoleExists(UserRoles.BusinessOwer)) context.Roles.AddOrUpdate(new Role() { Name = UserRoles.BusinessOwer, Description = UserRoles.BusinessOwer, ManageBusiness = true });
            if (!roleManager.RoleExists(UserRoles.User)) context.Roles.AddOrUpdate(new Role() { Name = UserRoles.User, Description = UserRoles.User });

            var admin = new User()
            {
                UserName = "admin",
                FirstName = "Khoa",
                LastName = "Ho Tan",
                Address = "Da Nang, Viet Nam"
            };

            var businessOwer = new User()
            {
                UserName = "business1",
                FirstName = "Phú Mỹ Phát",
                LastName = "ĐN",
                Address = "Hoi An, Viet Nam"
            };



            if (userManager.Find("admin", "abcde12345-") == null)
            {
                var resultAdmin = userManager.Create(admin, "abcde12345-");
                if (resultAdmin.Succeeded)
                    userManager.AddToRole(admin.Id, UserRoles.Administrator);
            }

            if (userManager.Find("business1", "abcde12345-") == null)
            {
                var resultMember = userManager.Create(businessOwer, "abcde12345-");
                if (resultMember.Succeeded)
                    userManager.AddToRole(businessOwer.Id, UserRoles.BusinessOwer);
            }



            #endregion


            #region Default Setting
            var defaultSetting = new Setting()
            {
                DefaultGLa = 37.344795F,
                DefaultGLng = -121.896377F,
                DefaultRadius = 0.5,
                DefaultZoom = 13,
                IsEnableChat = false,
                AllowLocationTracking = true
            };
            var isExisted = context.Settings.Count() > 0;
            if (isExisted == false)
            {
                context.Settings.Add(defaultSetting);
                context.SaveChanges();
            }
            #endregion

        }
    }
}