
using System.ComponentModel.DataAnnotations;

namespace Minibus.Data.Model
{
	public class RouteTemplate
	{
		public int Id { get; set; }

		[StringLength(100)]
		public string Name { get; set; }

		public virtual RouteBase RouteBase { get; set; }
	}
}
