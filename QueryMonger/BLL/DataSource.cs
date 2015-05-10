using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QueryMonger.DAL;

namespace QueryMonger.BLL
{
	public class DataSource
	{
		private DataSourceContext context;

		public DataSource()
		{
			context = new DataSourceContext();
		}

		public  DataTable RunScript(string script)
		{
			ICollection<object> result = new List<object>();

			SqlDataAdapter adapter = new SqlDataAdapter(script, (SqlConnection)context.Database.Connection);

			DataTable data = new DataTable();
			adapter.Fill(data);

			return data;
		}
	}
}