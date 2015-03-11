namespace Oas.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "DriverLicense", c => c.String());
            DropColumn("dbo.Drivers", "Drivericense");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drivers", "Drivericense", c => c.String());
            DropColumn("dbo.Drivers", "DriverLicense");
        }
    }
}
