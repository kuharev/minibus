using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Minibus.Data.Database;
using Minibus.Data.Model;
using Minibus.Web.Core;
using Minibus.Web.Models.Days;
using Minibus.Web.Models.Routes;

namespace Minibus.Web.Controllers.API
{
	public class RoutesController : ApiController
	{
		private readonly IRepository<Route> _routeRepository;
		private readonly IRepository<RouteTemplate> _routeTemplateRepository;
		private readonly IRepository<DayTemplate> _dayTemplateRepository;

		public RoutesController(IRepository<Route> routeRepository,
			IRepository<RouteTemplate> routeTemplateRepository,
			IRepository<DayTemplate> dayTemplateRepository)
		{
			_routeRepository = routeRepository;
			_routeTemplateRepository = routeTemplateRepository;
			_dayTemplateRepository = dayTemplateRepository;
		}

		[HttpGet, Route("api/routes/routes", Name = AppRoutes.RoutesApi.Routes)]
		public IEnumerable<RouteModel> Routes()
		{
			return _routeRepository.Get().ToList().Select(r => new RouteModel(r));
		}

		[HttpGet, Route("api/routes/routestemplates", Name = AppRoutes.RoutesApi.RoutesTemplates)]
		public IEnumerable<RouteTemplateModel> RoutesTemplates()
		{
			return _routeTemplateRepository.Get().ToList().Select(t => new RouteTemplateModel(t));
		}

		[HttpGet, Route("api/routes/daystemplates", Name = AppRoutes.RoutesApi.DaysTemplates)]
		public IEnumerable<DayTemplateModel> DaysTemplates()
		{
			return _dayTemplateRepository.Get().ToList().Select(t => new DayTemplateModel(t));
		}
	}
}
