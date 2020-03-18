namespace AuctionExpress.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IdentityUserRole");
            AlterColumn("dbo.IdentityUserRole", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.IdentityUserRole", new[] { "UserId", "RoleId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IdentityUserRole");
            AlterColumn("dbo.IdentityUserRole", "RoleId", c => c.String());
            AddPrimaryKey("dbo.IdentityUserRole", "UserId");
        }
    }
}
