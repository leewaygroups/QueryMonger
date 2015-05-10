using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace QueryMonger.UserManagement.Infrastructure
{
	public class ApplicationRoleManager: RoleManager<IdentityRole>
	{
		public ApplicationRoleManager(IRoleStore<IdentityRole, string> rolestore)
			: base(rolestore)
		{
		}

		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
		{
			var applicationRoleManager =
				new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationUserDbContext>()));

			return applicationRoleManager;
		}

	}
}