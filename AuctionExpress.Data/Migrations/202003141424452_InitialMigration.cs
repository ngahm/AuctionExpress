namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IdentityRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.IdentityRole", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Product", "MinimumSellingPrice", c => c.Double(nullable: false));
            DropColumn("dbo.IdentityRole", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IdentityRole", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Product", "MinimumSellingPrice");
            CreateIndex("dbo.IdentityRole", "ApplicationUser_Id");
            AddForeignKey("dbo.IdentityRole", "ApplicationUser_Id", "dbo.ApplicationUser", "Id");
        }
    }
}
