using Minibus.Data.Model;

namespace Minibus.Web.Models.Routes
{
	public class RouteModel : RouteBaseModel
	{
		public RouteModel() { }

		public RouteModel(Route route) : base(route.RouteBase)
		{
			
		}
	}
}