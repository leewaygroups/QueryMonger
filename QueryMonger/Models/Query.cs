using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace QueryMonger.Models
{
	public class Query
	{
		private DateTime _dateCreated = DateTime.Now;
		private DateTime _dateModified = DateTime.Now;
		public int QueryId { get; set; }
		public string UserName { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Script { get; set; }

		public DateTime CreatedOn
		{
			get { return _dateCreated;  }
			set { _dateCreated = value; }
		}

		public DateTime LastModifiedOn
		{
			get { return _dateModified; }
			set { _dateModified = value; }
		}

	}

    public class TinyQuery
    {
		[Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Script { get; set; }
        public string UserName { get; set; }
    }
}