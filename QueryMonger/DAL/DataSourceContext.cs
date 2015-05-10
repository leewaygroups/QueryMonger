using System.Data.Entity;

namespace QueryMonger.DAL
{
	public class DataSourceContext : DbContext
	{
		/// <summary>
		/// Context which stored scripts are executed against.
		/// </summary>
		public DataSourceContext()
			: base("PaySystem")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

	}
}