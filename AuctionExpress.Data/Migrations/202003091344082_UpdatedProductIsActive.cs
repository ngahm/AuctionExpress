namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductIsActive : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "ProductIsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductIsActive", c => c.Boolean(nullable: false));
        }
    }
}
