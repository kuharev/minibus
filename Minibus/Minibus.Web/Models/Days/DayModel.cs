
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Minibus.Data.Model;
using Minibus.Web.Models.Routes;

namespace Minibus.Web.Models.Days
{
	public class DayModel
	{
		public DayModel() { }

		public DayModel(Day day)
		{
			id = day.Id;
			date = day.Date.ToShortDateString();
			routes = Mapper.Map<List<RouteBase>, List<RouteBaseModel>>(day.Routes.ToList());
		}

		public int? id { get; set; }
		public string date { get; set; }
		public List<RouteBaseModel> routes { get; set; }
	}
}