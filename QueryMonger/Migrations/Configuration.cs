using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QueryMonger.DAL;
using QueryMonger.Models;
using QueryMonger.UserManagement.Infrastructure;

namespace QueryMonger.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QueryMonger.DAL.QueryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QueryMonger.DAL.QueryDbContext context)
        {
			//var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationUserDbContext()));
			//var rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationUserDbContext()));

			var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new QueryDbContext()));
			var rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new QueryDbContext()));

			var user1 = new ApplicationUser()
			{
				UserName = "Nikita",
				Email = "nikip@dojo.com",
				EmailConfirmed = true
			};

			var user2 = new ApplicationUser()
			{
				UserName = "Fouad",
				Email = "fouad@sharepoint.com",
				EmailConfirmed = true
			};

			var user3 = new ApplicationUser()
			{
				UserName = "Edgar",
				Email = "edgar@sharepoint.com",
				EmailConfirmed = true
			};

			var user4 = new ApplicationUser()
			{
				UserName = "Prince",
				Email = "prince@sharepoint.com",
				EmailConfirmed = true
			};

			var user5 = new ApplicationUser()
			{
				UserName = "Potjana",
				Email = "potjana@sharepoint.com",
				EmailConfirmed = true
			};

			var user6 = new ApplicationUser()
			{
				UserName = "SuperUser",
				Email = "lewaygroups@gmail.com",
				EmailConfirmed = true
			};


			manager.Create(user1, "Password1");
			manager.Create(user2, "Password2");
			manager.Create(user3, "Password1");
			manager.Create(user4, "Password2");
			manager.Create(user5, "Password1");
			manager.Create(user6, "superAdmin");

			if (rolesManager.Roles.Count() == 0)
			{
				rolesManager.Create(new IdentityRole { Name = "Admin" });
				rolesManager.Create(new IdentityRole { Name = "User" });
				rolesManager.Create(new IdentityRole { Name = "SuperAdmin" });
			}

			var superUser = manager.FindByName("SuperUser");
			manager.AddToRoles(superUser.Id, new string[] { "Admin", "SuperAdmin" });

			var queries = new List<Query>
           {
               new Query {
				   UserName = user1.UserName
				   ,Title = "Provident Fund Outpayment Requests"
				   ,Description = "List of provident fund outpayment request yet to be reported as paid by Zurich"
				   ,Script = "Select A.Id, B.Id, A.Firstname, A.LastName, B.ContractorUserId, B.Email from [UserManagement2].[dbo].[User] A INNER JOIN PaySystem.dbo.ProvidentFundOutPayment B ON A.id = B.ContractorUserId"
				   },

				   new Query {
					UserName = user5.UserName
				   ,Title = "Cleared Provident Fund Outpayment Requests"
				   ,Description = "List of provident fund outpayment request reported as paid by Zurich"
				   ,Script = "Select A.Id, B.Id, A.Firstname, A.LastName, B.ContractorUserId, B.Email from [UserManagement2].[dbo].[User] A INNER JOIN PaySystem.dbo.ProvidentFundOutPayment B ON A.id = B.ContractorUserId"
				   },
				   new Query {
					UserName = user3.UserName
				   ,Title = "Pending Provident Fund Outpayment Requests"
				   ,Description = "List of provident fund outpayment request yet to be sent to Zurich"
				   ,Script = "Select A.Id, B.Id, A.Firstname, A.LastName, B.ContractorUserId, B.Email from [UserManagement2].[dbo].[User] A INNER JOIN PaySystem.dbo.ProvidentFundOutPayment B ON A.id = B.ContractorUserId"
				   }
           };

			queries.ForEach(s => context.Queries.AddOrUpdate(q => q.Title, s));
			context.SaveChanges();
        }
    }
}
