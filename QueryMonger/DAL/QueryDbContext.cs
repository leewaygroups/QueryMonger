using System.Data.Entity;
using QueryMonger.Models;
using QueryMonger.UserManagement.Infrastructure;

namespace QueryMonger.DAL
{
	public class QueryDbContext : ApplicationUserDbContext //DbContext
	{
		public QueryDbContext()
			: base()
        {
			//QueryDbContext
			this.Configuration.LazyLoadingEnabled = true;
        }

		public DbSet<Query> Queries { get; set; }
	}
}