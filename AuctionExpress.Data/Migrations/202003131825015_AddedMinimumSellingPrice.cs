namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMinimumSellingPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "MinimumSellingPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "MinimumSellingPrice");
        }
    }
}
