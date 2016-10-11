using Minibus.Data.Model;

namespace Minibus.Web.Models.Routes
{
	public class RouteTemplateModel : RouteBaseModel
	{
		public RouteTemplateModel() { }

		public RouteTemplateModel(RouteTemplate template) : base(template.RouteBase)
		{
			templateId = template.Id;
			name = template.Name;
		}

		public int? templateId { get; set; }
		public string name { get; set; }
	}
}