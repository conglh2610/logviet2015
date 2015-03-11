namespace Oas.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tieucongcong : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CarAccidents", new[] { "CarCustomer_Id" });
            DropIndex("dbo.CarBookings", new[] { "CarCustomer_Id" });
            CreateTable(
                "dbo.CarCustomers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.CarAccidents", "CarCustomer_Id", c => c.Guid());
            AlterColumn("dbo.CarBookings", "CarCustomer_Id", c => c.Guid());
            CreateIndex("dbo.CarBookings", "CarCustomer_Id");
            CreateIndex("dbo.CarAccidents", "CarCustomer_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CarAccidents", new[] { "CarCustomer_Id" });
            DropIndex("dbo.CarBookings", new[] { "CarCustomer_Id" });
            AlterColumn("dbo.CarBookings", "CarCustomer_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.CarAccidents", "CarCustomer_Id", c => c.String(maxLength: 128));
            DropTable("dbo.CarCustomers");
            CreateIndex("dbo.CarBookings", "CarCustomer_Id");
            CreateIndex("dbo.CarAccidents", "CarCustomer_Id");
        }
    }
}
