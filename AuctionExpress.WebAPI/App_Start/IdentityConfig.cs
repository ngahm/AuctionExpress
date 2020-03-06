using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using AuctionExpress.WebAPI.Models;
using AuctionExpress.Data;
using System.Linq;

namespace AuctionExpress.WebAPI
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            using (var ctx = new ApplicationDbContext())
            {
            var entity =
              ctx
                .Users
                  .Single(e => e.Id==user.Id);

            entity.IsActive = false;

            ctx.SaveChanges();
            }
            //  bool wasSaved = (ctx.SaveChangesAsync().Result==1);
            //Need to save this to database

            return Task.FromResult(IdentityResult.Success);
            // }

        }
        // public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
        //{
        //  public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout)
        //{
        //  var user = UserManager.FindByEmailAsync(userName).Result;

        //if (!user.IsActive)
        // {
        //   return Task.FromResult<SignInStatus>(SignInStatus.LockedOut);
        // }

        //  return base.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
        // }
    }
}
