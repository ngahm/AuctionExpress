namespace AuctionExpress.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AuctionExpress.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuctionExpress.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            ////       context.Product.AddOrUpdate(
            //      p => p.ProductStartTime,
            //      new Product { ProductStartTime = DateTimeOffset.Now.AddDays(5) },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            context.Users.AddOrUpdate(
                x => x.UserName,
                new ApplicationUser() { Id = Guid.NewGuid().ToString(), Email = "Admin@AuctionExpress.com", UserName = "Admin", BusinessName = "AuctionExpress", PasswordHash = new PasswordHasher().HashPassword("@dmin1") },
                new ApplicationUser() { Id = Guid.NewGuid().ToString(), Email = "TestActiveUser@AuctionExpress.com", UserName = "TestActiveUser", BusinessName = "AuctionExpress", PasswordHash = new PasswordHasher().HashPassword("TestActiveUser") },
                new ApplicationUser() { Id = Guid.NewGuid().ToString(), Email = "User1@AuctionExpress.com", UserName = "User1", BusinessName = "AuctionExpress", PasswordHash = new PasswordHasher().HashPassword("User1") },
                new ApplicationUser() { Id = Guid.NewGuid().ToString(), Email = "User2@AuctionExpress.com", UserName = "User2", BusinessName = "AuctionExpress", PasswordHash = new PasswordHasher().HashPassword("User2") },
                new ApplicationUser() { Id = Guid.NewGuid().ToString(), Email = "User3@AuctionExpress.com", UserName = "User3", BusinessName = "AuctionExpress", PasswordHash = new PasswordHasher().HashPassword("User3") },
                new ApplicationUser() { Id = Guid.NewGuid().ToString(), Email = "TestActiveUser@AuctionExpress.com", UserName = "TestActiveUser", BusinessName = "AuctionExpress", PasswordHash = new PasswordHasher().HashPassword("TestActiveUser") },
                new ApplicationUser() { Id = Guid.NewGuid().ToString(), Email = "TestInActiveUser@AuctionExpress.com", UserName = "TestInActiveUser", BusinessName = "AuctionExpress", IsActive=false, PasswordHash = new PasswordHasher().HashPassword("TestInActiveUser") });

            context.SaveChanges();

            List<string> userNames = new List<string>() { "Admin", "TestActiveUser", "TestInActiveUser" };
            foreach (var user in userNames)
            {
                string userId = context.Users.Where(x => x.UserName == user && string.IsNullOrEmpty(x.SecurityStamp)).Select(x => x.Id).FirstOrDefault();

                if (!string.IsNullOrEmpty(userId))
                    userManager.UpdateSecurityStamp(userId);

                context.SaveChanges();
            }

            context.Category.AddOrUpdate(

                x => x.CategoryName,
                new Category() { CategoryName = "Office Chairs" },
                new Category() { CategoryName = "Desks" },
                new Category() { CategoryName = "Tables" },
                new Category() { CategoryName = "Filing Cabinets" });
            context.SaveChanges();

            //context.Product.AddOrUpdate(
            //    x=>x.ProductName,
            //    new Product(){ProductName="Executive Leather Office Chairs",ProductDescription="Like new black leather swivel chairs with armrests.",ProductQuantity=10,ProductStartTime=DateTimeOffset.Now.AddDays(-4),ProductCloseTime =DateTimeOffset.Now.AddDays(+5),

            //    )
            
        }
    }
}
