using System.Collections.Generic;
using System.Security.Claims;

namespace QueryMonger.UserManagement.Infrastructure
{
	public static class ExtendedClaimsProvider
	{
		public static IEnumerable<Claim> GetClaims(ApplicationUser user)
		{
			List<Claim> claims = new List<Claim>();

			if (user.EmailConfirmed)
			{
				claims.Add(CreateClaims("AccountActivated", "1"));
			}

			return claims;
		}

		private static Claim CreateClaims(string type, string value)
		{
			return  new Claim(type, value, ClaimValueTypes.String);
		}

	}
}