using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace QueryMonger.UserManagement.Infrastructure
{
    public class RolesFromClaims
    {
        public static IEnumerable<Claim> CreateRolesBasedOnClaims(ClaimsIdentity identity)
        {
            List<Claim> claims = new List<Claim>();

            if (identity.HasClaim(c => c.Type == "AccountActivated" && c.Value == "1") &&
                (identity.HasClaim(ClaimTypes.Role, "User") || identity.HasClaim(ClaimTypes.Role, "Admin") ||
                identity.HasClaim(ClaimTypes.Role, "SuperUser")))
            {
                claims.Add(new Claim(ClaimTypes.Role, "ReportUser"));
            }

            return claims;
        }
    }
}