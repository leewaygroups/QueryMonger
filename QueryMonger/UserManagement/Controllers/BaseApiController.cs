using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QueryMonger.UserManagement.Models;
using QueryMonger.UserManagement.Infrastructure;
using QueryMonger.BLL;


namespace QueryMonger.UserManagement.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private ApplicationUserManager _ApplicationUserManager = null;
        private QueryManager _queryManager = null;
        private ApplicationRoleManager _ApplicationRoleManager = null;

        protected ApplicationRoleManager ApplicationRoleManager
        {
            get
            {
                return _ApplicationRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public QueryManager QueryManager
        {
            get { return _queryManager ?? new QueryManager(); }
        }

        public ApplicationUserManager ApplicationUserManager
        {
            get
            {
                return _ApplicationUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

      public ModelFactory TheModelFactory
        {
            get
            {
	            
	            return _modelFactory ?? (_modelFactory = new ModelFactory(this.Request, this.ApplicationUserManager));
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
