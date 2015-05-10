using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using QueryMonger.UserManagement.Services;

namespace QueryMonger.UserManagement.Infrastructure
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            :base(store)
        {
        }

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var appDbContext = context.Get<ApplicationUserDbContext>();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength =  6,
				RequireNonLetterOrDigit = true,
				RequireDigit =  false
			};

			manager.EmailService = new EmailService();
 
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
					new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
					{
						TokenLifespan = TimeSpan.FromHours(6)
					};
			}

            return manager;
        }
    }
}