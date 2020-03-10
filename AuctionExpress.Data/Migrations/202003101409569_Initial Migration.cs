namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bid", "TimeOfBid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bid", "TimeOfBid", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
