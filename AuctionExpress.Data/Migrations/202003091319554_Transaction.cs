namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transaction : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transaction", "PaymentDate", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transaction", "PaymentDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
