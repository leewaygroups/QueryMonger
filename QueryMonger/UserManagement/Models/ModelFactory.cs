using Microsoft.AspNet.Identity.EntityFramework;
using QueryMonger.UserManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Routing;

namespace QueryMonger.UserManagement.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ApplicationUserManager _ApplicationUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager applicationUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _ApplicationUserManager = applicationUserManager;
        }

        public UserReturnModel Create(ApplicationUser user)
        {
	        var test = _ApplicationUserManager;
	        var val = test;


			return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = user.Id }),
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Roles = _ApplicationUserManager.GetRolesAsync(user.Id).Result,
                Claims = _ApplicationUserManager.GetClaimsAsync(user.Id).Result,
            };
        }

        public RoleReturnModel Create(IdentityRole appRole)
        {

            return new RoleReturnModel
            {
                Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id = appRole.Id,
                Name = appRole.Name
            };

        }
    }

    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }
    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}