namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction");
            DropIndex("dbo.Product", new[] { "ProductTransactionKey" });
            AlterColumn("dbo.Product", "ProductTransactionKey", c => c.Int());
            CreateIndex("dbo.Product", "ProductTransactionKey");
            AddForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction", "TransactionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction");
            DropIndex("dbo.Product", new[] { "ProductTransactionKey" });
            AlterColumn("dbo.Product", "ProductTransactionKey", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "ProductTransactionKey");
            AddForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction", "TransactionId", cascadeDelete: true);
        }
    }
}
