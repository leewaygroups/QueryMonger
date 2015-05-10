using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using QueryMonger.UserManagement.Controllers;
using QueryMonger.UserManagement.Infrastructure;
using QueryMonger.UserManagement.Models;

namespace QueryMonger.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
		//[Authorize(Roles="Admin")]
		[Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.ApplicationUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Authorize(Roles = "Admin")]
		[Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await this.ApplicationUserManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();
        }

	    [Authorize(Roles = "Admin")]
		[Route("user/{username}", Name = "GetUserByName")]
		public async Task<IHttpActionResult> GetUserByName(string name)
	    {
		    var user = await this.ApplicationUserManager.FindByNameAsync(name);
		    if (user == null)
		    {
			    return NotFound();
		    }

		    return Ok(this.TheModelFactory.Create(user));
	    }

	    [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email
			};

           IdentityResult adduserresult = await this.ApplicationUserManager.CreateAsync(user, createUserModel.Password);

            if (!adduserresult.Succeeded)
            {
                return GetErrorResult(adduserresult);
            }

	        string code = await this.ApplicationUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

	        var callBackUrl = new Uri(Url.Link("ConfirmEmailRoute", new {userId = user.Id, code = code}));

	        await this.ApplicationUserManager.SendEmailAsync(user.Id, "Confirm your account",
			        "Kindly confirm your account by clicking <a href=\"" + callBackUrl + "\">here</a>");

            Uri locationheader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationheader, TheModelFactory.Create(user));
        }

		[AllowAnonymous]
        [HttpGet]
		[Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
		public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
	    {
			if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
			{
				ModelState.AddModelError("", "UserId and Code not found");

				return BadRequest(ModelState);
			}

			IdentityResult result = await this.ApplicationUserManager.ConfirmEmailAsync(userId, code);

			if (result.Succeeded)
			{
                return Ok();
			}
			else
			{
				return GetErrorResult(result);
			}
	    }

	    [Authorize]
        [Route("ChangePassword")]
		public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
	    {
		    if (!ModelState.IsValid)
		    {
			    return BadRequest(ModelState);
		    }

		    IdentityResult result =
			    await this.ApplicationUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

		    if (!result.Succeeded)
		    {
			    return GetErrorResult(result);
		    }

		    return Ok();
	    }

	    [Authorize(Roles="Admin")]
        [Route("user/{id:guid}")]
		public async Task<IHttpActionResult> DeleteUser(string id)
	    {
		    var user = await this.ApplicationUserManager.FindByIdAsync(id);
		    if (user != null)
		    {
			    IdentityResult result = await this.ApplicationUserManager.DeleteAsync(user);

			    if (!result.Succeeded)
			    {
				    return GetErrorResult(result);
			    }

			    return Ok();
		    }

		    return NotFound();
	    }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            var appUser = await this.ApplicationUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await this.ApplicationUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(this.ApplicationRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.ApplicationUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.ApplicationUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();

        }
    }
}
