using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using QueryMonger.UserManagement.Controllers;

namespace QueryMonger.Controllers
{
    public class ClaimsController : BaseApiController
    {
        public IHttpActionResult GetClaims()
        {
            var identity = User.Identity as ClaimsIdentity;


            if (identity == null) return null;
            var claims = from c in identity.Claims
                select new
                {
                    subject = c.Subject.Name,
                    type = c.Type,
                    value = c.Value
                };

            return Ok(claims);
        }
    }
}