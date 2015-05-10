using Microsoft.AspNet.Identity.EntityFramework;

namespace QueryMonger.UserManagement.Infrastructure
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext()
			: base("QueryMonger")
        {
			//UserManagement
        }

		//public DbSet<Query> Queries { get; set; }

        public static ApplicationUserDbContext Create()
        {
            return new ApplicationUserDbContext();
        }
    }
}