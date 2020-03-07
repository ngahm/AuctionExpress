namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewProductProps : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transaction", "BidId", "dbo.Bid");
            DropForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction");
            DropIndex("dbo.Product", new[] { "ProductTransactionKey" });
            DropIndex("dbo.Transaction", new[] { "BidId" });
            AddColumn("dbo.Transaction", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transaction", "ProductId");
            AddForeignKey("dbo.Transaction", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            DropColumn("dbo.Product", "ProductTransactionKey");
            DropColumn("dbo.Transaction", "BidId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaction", "BidId", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "ProductTransactionKey", c => c.Int());
            DropForeignKey("dbo.Transaction", "ProductId", "dbo.Product");
            DropIndex("dbo.Transaction", new[] { "ProductId" });
            DropColumn("dbo.Transaction", "ProductId");
            CreateIndex("dbo.Transaction", "BidId");
            CreateIndex("dbo.Product", "ProductTransactionKey");
            AddForeignKey("dbo.Product", "ProductTransactionKey", "dbo.Transaction", "TransactionId");
            AddForeignKey("dbo.Transaction", "BidId", "dbo.Bid", "BidId", cascadeDelete: true);
        }
    }
}
