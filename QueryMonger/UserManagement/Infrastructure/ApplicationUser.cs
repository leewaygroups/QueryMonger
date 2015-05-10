using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QueryMonger.Models;

namespace QueryMonger.UserManagement.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        //Add more properties if need be.

		public bool CanExecuteQuery { get; set; }

		public virtual ICollection<Query> Queries { get; set; } 

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }
    }
}