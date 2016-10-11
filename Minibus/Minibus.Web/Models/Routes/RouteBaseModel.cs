using System.Collections.Generic;
using Minibus.Data.Model;

namespace Minibus.Web.Models.Routes
{
	public class RouteBaseModel
	{
		public RouteBaseModel() { }

		public RouteBaseModel(RouteBase route)
		{
			id = route.Id;
			time = route.Time;
			from = route.From;
			to = route.To;
			carNumber = route.CarNumber;
			placesCount = route.PlaceNumber;
			driver = route.DriverFullName;
			driverPhone = route.DriverPhone;
			if(route.Stops != null)
			stops = new List<string>(route.Stops);
		}

		public int? id { get; set; }
		public string time { get; set; }
		public string from { get; set; }
		public string to { get; set; }
		public string carNumber { get; set; }
		public int placesCount { get; set; }
		public string driver { get; set; }
		public string driverPhone { get; set; }
		public List<string> stops { get; set; }
	}
}