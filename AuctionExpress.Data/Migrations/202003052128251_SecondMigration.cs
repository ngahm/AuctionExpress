namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ProductCategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "ProductCategoryId" });
            AlterColumn("dbo.Product", "ProductCategoryId", c => c.Int());
            CreateIndex("dbo.Product", "ProductCategoryId");
            AddForeignKey("dbo.Product", "ProductCategoryId", "dbo.Category", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "ProductCategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "ProductCategoryId" });
            AlterColumn("dbo.Product", "ProductCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "ProductCategoryId");
            AddForeignKey("dbo.Product", "ProductCategoryId", "dbo.Category", "CategoryId", cascadeDelete: true);
        }
    }
}
