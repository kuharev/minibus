
using System;
using System.Collections.Generic;

namespace Minibus.Data.Model
{
	public class Day
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public virtual ICollection<RouteBase> Routes { get; set; }
	}
}
