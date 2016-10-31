using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CourseManagementSystem.Models
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        {

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            AppDbContext db = context.Get<AppDbContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<User>(db));
            manager.UserTokenProvider =
                new DataProtectorTokenProvider<User>(options.DataProtectionProvider.Create("ASP.NET Identity"));
            manager.EmailService = new EmailNotifications();
            return manager;
        }
    }
}