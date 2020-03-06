namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductTransactionFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction");
            DropIndex("dbo.Product", new[] { "ProductTransactionKey" });
            AddColumn("dbo.Transaction", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transaction", "ProductId");
            AddForeignKey("dbo.Transaction", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            DropColumn("dbo.Product", "ProductTransactionKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductTransactionKey", c => c.Int());
            DropForeignKey("dbo.Transaction", "ProductId", "dbo.Product");
            DropIndex("dbo.Transaction", new[] { "ProductId" });
            DropColumn("dbo.Transaction", "ProductId");
            CreateIndex("dbo.Product", "ProductTransactionKey");
            AddForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction", "TransactionId");
        }
    }
}
