using System.Collections.Generic;

namespace Minibus.Data.Model
{
	public class RouteBase
	{
		public int Id { get; set; }
		public string Time { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string CarNumber { get; set; }
		public int PlaceNumber { get; set; }
		public string DriverFullName { get; set; }
		public string DriverPhone { get; set; }
		public List<string> Stops { get; set; }

		public int? DayTemplateId { get; set; }
        public int? DayId { get; set; }
	}
}
