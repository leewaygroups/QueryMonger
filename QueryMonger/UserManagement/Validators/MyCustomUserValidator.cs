using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using QueryMonger.UserManagement.Infrastructure;

namespace QueryMonger.UserManagement.Validators
{
	public class MyCustomUserValidator : UserValidator<ApplicationUser>
	{
		List<string> _allowedEmailDomains = new List<string>{"unops.org"};

		public MyCustomUserValidator(ApplicationUserManager applicationUserManager)
			: base(applicationUserManager)
		{
		}

		public override  async Task<IdentityResult> ValidateAsync(ApplicationUser user)
		{
			IdentityResult result = await base.ValidateAsync(user);

			var emailDomain = user.Email.Split('@')[1];
			if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
			{
				var error = result.Errors.ToList();

				error.Add(string.Format("Email domain '{0}' is not allowd", emailDomain));

				result = new IdentityResult(error);
			}

			return result;
		}
	}
}