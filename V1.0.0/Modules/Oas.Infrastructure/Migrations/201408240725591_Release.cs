namespace Oas.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Release : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        Url = c.String(),
                        Description = c.String(),
                        ClickCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Position = c.Int(nullable: false),
                        TotalClicked = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Name = c.String(),
                        GooglePlaceIconUrl = c.String(),
                        CategoryId = c.Int(nullable: false),
                        CategoryParentId = c.Int(),
                        EN = c.String(),
                        KR = c.String(),
                        ZH_HANT = c.String(),
                        ZH = c.String(),
                        JP = c.String(),
                        PT = c.String(),
                        DE = c.String(),
                        IT = c.String(),
                        ES = c.String(),
                        FR = c.String(),
                        IsAbstract = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                        BusinessCategoryId = c.Guid(nullable: false),
                        IsAlcohol = c.Boolean(nullable: false),
                        IsAlcohol_bar = c.Boolean(nullable: false),
                        IsAlcohol_beer_wine = c.Boolean(nullable: false),
                        Attire = c.String(),
                        Country = c.String(),
                        Locality = c.String(),
                        Meal_breakfast = c.Boolean(nullable: false),
                        Meal_cater = c.Boolean(nullable: false),
                        Meal_deliver = c.Boolean(nullable: false),
                        Meal_takeout = c.Boolean(nullable: false),
                        Open_24hrs = c.Boolean(nullable: false),
                        Options_healthy = c.Boolean(nullable: false),
                        Options_vegetarian = c.Boolean(nullable: false),
                        Payment_cashonly = c.Boolean(nullable: false),
                        Postcode = c.String(),
                        Price = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Region = c.String(),
                        Wifi = c.Boolean(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        UpdatedDate = c.Boolean(nullable: false),
                        Zipcode = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Information = c.String(),
                        SortDescription = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Status = c.Int(nullable: false),
                        Latitude = c.Double(),
                        Longtitude = c.Double(),
                        Active = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        TotalViewed = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessCategories", t => t.BusinessCategoryId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BusinessCategoryId);
            
            CreateTable(
                "dbo.BusinessComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                        BusinessId = c.Guid(nullable: false),
                        BusinessRate = c.Int(nullable: false),
                        Comment = c.String(),
                        CreateDate = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        ProfileImage = c.String(),
                        AccountType = c.Int(),
                        Suspend = c.Boolean(),
                        Tips = c.String(),
                        Gender = c.Int(),
                        ContactTitle = c.String(),
                        MembershipPackageId = c.Guid(),
                        StartDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                        Status = c.Int(),
                        PackagePrice = c.Decimal(precision: 18, scale: 2),
                        PaymentMethod = c.Int(),
                        PaymentPeriod = c.Int(),
                        IsOnline = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MembershipPackages", t => t.MembershipPackageId)
                .Index(t => t.MembershipPackageId);
            
            CreateTable(
                "dbo.Checkins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        BusinessId = c.Guid(nullable: false),
                        CheckinDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.BusinessId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.MembershipPackages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OldPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(),
                        FromUserId = c.String(maxLength: 128),
                        ToUserId = c.String(maxLength: 128),
                        Status = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromUserId)
                .ForeignKey("dbo.Users", t => t.ToUserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BusinessId = c.Guid(nullable: false),
                        Caption = c.String(),
                        Url = c.String(),
                        IsProfileImage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.Category_ZipCode",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BusinessCategoryId = c.Guid(nullable: false),
                        ZipcodeId = c.Guid(nullable: false),
                        CategoryName = c.String(),
                        CategoryId = c.Int(nullable: false),
                        ZipcodeName = c.String(),
                        TotalRecord = c.Int(nullable: false),
                        Downloaded = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessCategories", t => t.BusinessCategoryId)
                .ForeignKey("dbo.ZipCodes", t => t.ZipcodeId)
                .Index(t => t.BusinessCategoryId)
                .Index(t => t.ZipcodeId);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Zip = c.String(),
                        City = c.String(),
                        County = c.String(),
                        State = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessClosedReports",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BusinessId = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ReportDate = c.DateTime(nullable: false),
                        Verified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BusinessId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BusinessLikes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                        BusinessId = c.Guid(nullable: false),
                        Like = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.BusinessMenus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BusinessId = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.BusinessPromotions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BusinessId = c.Guid(nullable: false),
                        PromotionId = c.Guid(nullable: false),
                        Description = c.String(),
                        Discount = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Limitation = c.Int(nullable: false),
                        Viewed = c.Int(nullable: false),
                        Redeemed = c.Int(nullable: false),
                        PromotionCode = c.String(),
                        ShowPromotionCode = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .ForeignKey("dbo.Promotions", t => t.PromotionId)
                .Index(t => t.BusinessId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NeighborHoods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BusinessId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.OpenBusinessSchedulers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BusinessId = c.Guid(nullable: false),
                        Monday = c.String(),
                        Tuesday = c.String(),
                        Wednesday = c.String(),
                        Thursday = c.String(),
                        Friday = c.String(),
                        Saturday = c.String(),
                        Sunday = c.String(),
                        HoursDisplay = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.PackageItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        MembershipPackageId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MembershipPackages", t => t.MembershipPackageId)
                .Index(t => t.MembershipPackageId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        ManageUser = c.Boolean(),
                        ManageBusiness = c.Boolean(),
                        ManageMembershipPackage = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DefaultGLng = c.Single(),
                        DefaultGLa = c.Single(),
                        DefaultRadius = c.Double(nullable: false),
                        IsEnableChat = c.Boolean(nullable: false),
                        DefaultZoom = c.Int(nullable: false),
                        AllowLocationTracking = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PackageItems", "MembershipPackageId", "dbo.MembershipPackages");
            DropForeignKey("dbo.OpenBusinessSchedulers", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.NeighborHoods", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessPromotions", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.BusinessPromotions", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessMenus", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.BusinessLikes", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.BusinessClosedReports", "UserId", "dbo.Users");
            DropForeignKey("dbo.BusinessClosedReports", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Category_ZipCode", "ZipcodeId", "dbo.ZipCodes");
            DropForeignKey("dbo.Category_ZipCode", "BusinessCategoryId", "dbo.BusinessCategories");
            DropForeignKey("dbo.Images", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.MessageHistories", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MessageHistories", "ToUserId", "dbo.Users");
            DropForeignKey("dbo.MessageHistories", "FromUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "MembershipPackageId", "dbo.MembershipPackages");
            DropForeignKey("dbo.Checkins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Checkins", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Businesses", "UserId", "dbo.Users");
            DropForeignKey("dbo.BusinessComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.BusinessComments", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Businesses", "BusinessCategoryId", "dbo.BusinessCategories");
            DropForeignKey("dbo.BusinessCategories", "ParentId", "dbo.BusinessCategories");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.PackageItems", new[] { "MembershipPackageId" });
            DropIndex("dbo.OpenBusinessSchedulers", new[] { "BusinessId" });
            DropIndex("dbo.NeighborHoods", new[] { "BusinessId" });
            DropIndex("dbo.BusinessPromotions", new[] { "PromotionId" });
            DropIndex("dbo.BusinessPromotions", new[] { "BusinessId" });
            DropIndex("dbo.BusinessMenus", new[] { "BusinessId" });
            DropIndex("dbo.BusinessLikes", new[] { "BusinessId" });
            DropIndex("dbo.BusinessLikes", new[] { "UserId" });
            DropIndex("dbo.BusinessClosedReports", new[] { "UserId" });
            DropIndex("dbo.BusinessClosedReports", new[] { "BusinessId" });
            DropIndex("dbo.Category_ZipCode", new[] { "ZipcodeId" });
            DropIndex("dbo.Category_ZipCode", new[] { "BusinessCategoryId" });
            DropIndex("dbo.Images", new[] { "BusinessId" });
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.MessageHistories", new[] { "User_Id" });
            DropIndex("dbo.MessageHistories", new[] { "ToUserId" });
            DropIndex("dbo.MessageHistories", new[] { "FromUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Checkins", new[] { "User_Id" });
            DropIndex("dbo.Checkins", new[] { "BusinessId" });
            DropIndex("dbo.Users", new[] { "MembershipPackageId" });
            DropIndex("dbo.BusinessComments", new[] { "BusinessId" });
            DropIndex("dbo.BusinessComments", new[] { "UserId" });
            DropIndex("dbo.Businesses", new[] { "BusinessCategoryId" });
            DropIndex("dbo.Businesses", new[] { "UserId" });
            DropIndex("dbo.BusinessCategories", new[] { "ParentId" });
            DropTable("dbo.Settings");
            DropTable("dbo.Roles");
            DropTable("dbo.PackageItems");
            DropTable("dbo.OpenBusinessSchedulers");
            DropTable("dbo.NeighborHoods");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.Promotions");
            DropTable("dbo.BusinessPromotions");
            DropTable("dbo.BusinessMenus");
            DropTable("dbo.BusinessLikes");
            DropTable("dbo.BusinessClosedReports");
            DropTable("dbo.ZipCodes");
            DropTable("dbo.Category_ZipCode");
            DropTable("dbo.Images");
            DropTable("dbo.UserRoles");
            DropTable("dbo.MessageHistories");
            DropTable("dbo.MembershipPackages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Checkins");
            DropTable("dbo.Users");
            DropTable("dbo.BusinessComments");
            DropTable("dbo.Businesses");
            DropTable("dbo.BusinessCategories");
            DropTable("dbo.Advertisments");
        }
    }
}
