
using System;

namespace Minibus.Data.Model
{
	public class Route
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public RouteBase RouteBase { get; set; }
	}
}
