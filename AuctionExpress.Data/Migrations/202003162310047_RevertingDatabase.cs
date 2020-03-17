namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertingDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "ProductPhotoPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductPhotoPath", c => c.String());
        }
    }
}
