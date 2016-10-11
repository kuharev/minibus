
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Minibus.Data.Model
{
	public class DayTemplate
	{
		public int Id { get; set; }

		[StringLength(100)]
		public string Name { get; set; }

		public virtual ICollection<RouteBase> Routes { get; set; }
	}
}
