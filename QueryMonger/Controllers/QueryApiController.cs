using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using QueryMonger.BLL;
using QueryMonger.Models;
using System.Threading.Tasks;
using QueryMonger.UserManagement.Controllers;

namespace QueryMonger.Controllers
{
    /// <summary>
    /// Api controller
    /// </summary>
    [RoutePrefix("api")]
	public class QueryApiController : BaseApiController
    {
		/// <summary>
        /// Get all queries
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles="ReportUser")]
        [HttpGet]
        [Route("queries")]
		public IQueryable<Query> GetAllQueries()
        {
	        var queryList = this.QueryManager.GetAllQueries();
	        return queryList;
        }

        /// <summary>
        /// To get specific query details when end user selecting it.
        /// </summary>
        /// <param name="Id">The Id of the query</param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        [Route("query/{id}", Name = "GetQueryById")]
        public Query GetQueryById(int Id = 0)
        {
			return Id == 0 ? null : this.QueryManager.GetQueryById(Id);
        }

	   	/// <summary>
		/// Create query 
		/// </summary>
        /// <param name="query"> A TinyQuery object: Used to match the main & corresponding query in DB</param>
		/// <returns></returns>
        //[Authorize(Roles="Admin")]
        [Route("createQuery")]
		[HttpPost]
        [ResponseType(typeof(TinyQuery))]
		public async Task<IHttpActionResult> CreateQuery(TinyQuery tQuery)
		{
            int queryId = 0;

            if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            tQuery.UserName = RequestContext.Principal.Identity.Name;

            try
            {
                queryId = await this.QueryManager.CreateQueryAsync(tQuery);
            }
            catch(Exception ex)
            {
                //May do something
                return BadRequest();
            }

            Uri locationheader = new Uri(Url.Link("GetQueryById", new { id = queryId }));

            return Created(locationheader, tQuery);
		}

        ///<summary>
        ///Edit existing query
        ///</summary>
        /// <param name="Id">UserId of the query owner</param>
        /// <param name="query"> A TinyQuery object: Used to match the main & corresponding query in DB</param>
        ///<returns></returns>
        //[Authorize(Roles="Admin")]
		[Route("edit/{query.QueryId}", Name = "EditQuery")]
        [HttpPut]
        public IHttpActionResult EditQuery(Query query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.QueryManager.EditQuery(query);

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Delete existing query
        /// </summary>
        /// <param name="Id">UserId of the query owner</param>
        /// <param name="query"> A TinyQuery object: Used to match the main & corresponding query in DB</param>
        /// <returns></returns>
        //[Authorize(Roles="Admin")]
        [Route("delete/{id}", Name="Delete")]
        [HttpDelete]
        public IHttpActionResult DeleteQuery(int Id)
        {
	        this.QueryManager.DeleteQuery(Id);

	        return Ok();
        }

	    /// <summary>
	    /// API to execute selected query
	    /// </summary>
	    /// <param name="Id"></param>
	    /// <returns></returns>
        //[Authorize(Roles = "User")]
        [Route("report/{id}")]
        [HttpGet]
	    [ResponseType(typeof (Object))]
		public DataTable ExecuteQuery(int Id)
	    {
		   var test = this.QueryManager.ExecuteQuery(Id);
		    return test;
	    }
    }
}