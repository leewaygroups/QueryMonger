using System.Data;
using System.Data.Entity;
using System.Linq;
using QueryMonger.DAL;
using QueryMonger.Models;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace QueryMonger.BLL
{
    public class QueryManager
    {
        private readonly QueryDbContext _context = new QueryDbContext();

        public IQueryable<Query> GetAllQueries()
        {
            return _context.Queries;
        }

        public Query GetQueryById(int id)
        {
            return _context.Queries.SingleOrDefault(s => s.QueryId == id);
        }

        public async Task<int> CreateQueryAsync(TinyQuery tQuery)
        {
            var query = new Query { 
                Title = tQuery.Title,
                Description = tQuery.Description,
                Script = tQuery.Script,
                UserName = tQuery.UserName
            };

			_context.Queries.Add(query);
            await _context.SaveChangesAsync();

			return query.QueryId;
        }

        public void EditQuery(Query query)
        {
			var oldQuery = GetQueryById(query.QueryId);

	        if (oldQuery == null) return;

	        oldQuery.Title = query.Title;
	        oldQuery.Description = query.Description;
	        oldQuery.Script = query.Script;

	        _context.Entry(oldQuery).State = EntityState.Modified;

	        _context.SaveChanges();
        }

        public void DeleteQuery(int Id) 
        {
            var query = _context.Queries.FirstOrDefault(s => s.QueryId == Id);

	        if (query == null) return;

	        _context.Queries.Remove(query);

	        _context.SaveChanges();
        }

		public DataTable ExecuteQuery(int id)
	    {
		    var query = GetQueryById(id);
		   
			return query == null ? null : new DataSource().RunScript(query.Script);
	    }
    }
}