using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Minibus.Data.Model;
using Minibus.Web.Models.Routes;

namespace Minibus.Web.Models.Days
{
	public class DayTemplateModel
	{
		public DayTemplateModel() { }

		public DayTemplateModel(DayTemplate template)
		{
			templateId = template.Id;
			name = template.Name;
			routes = Mapper.Map<List<RouteBase>, List<RouteBaseModel>>(template.Routes.ToList());
		}

		public int? templateId { get; set; }
		public string name { get; set; }
		public List<RouteBaseModel> routes { get; set; }
	}
}