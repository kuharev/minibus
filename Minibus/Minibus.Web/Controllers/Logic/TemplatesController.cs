using System.Web.Mvc;
using AutoMapper;
using Minibus.Data.Database;
using Minibus.Data.Model;
using Minibus.Web.Core;
using Minibus.Web.Models.Days;
using Minibus.Web.Models.Routes;
using System.Linq;

namespace Minibus.Web.Controllers.Logic
{
	public class TemplatesController : Controller
	{
		private readonly IRepository<RouteTemplate> _routeTemplateRepository;
		private readonly IRepository<DayTemplate> _dayTemplateRepository;
		private readonly IRepository<RouteBase> _routeBaseRepository;

		public TemplatesController(IRepository<RouteTemplate> routeTemplateRepository, IRepository<DayTemplate> dayTemplateRepository, IRepository<RouteBase> routeBaseRepository)
		{
			_routeTemplateRepository = routeTemplateRepository;
			_dayTemplateRepository = dayTemplateRepository;
			_routeBaseRepository = routeBaseRepository;
		}

		[HttpGet, Route("templates/operator", Name = AppRoutes.Templates.Operator)]
		public PartialViewResult OperatorTemplates()
		{
			return PartialView();
		}

		[HttpPost, Route("templates/createroutetemplate", Name = AppRoutes.Templates.CreateRouteTemplate)]
		public JsonResult CreateRouteTemplate(RouteTemplateModel rModel)
		{
			if(!ModelState.IsValid)
				return Json(new { ok = false, message = "Validation has failed!" });

			var routeTemplate = Mapper.Map<RouteTemplate>(rModel);
			_routeTemplateRepository.Add(routeTemplate);
			_routeTemplateRepository.SaveChanges();

			return Json(new {ok = true, templateId = routeTemplate.Id, id = routeTemplate.RouteBase.Id});
		}

		[HttpPost, Route("templates/saveroutetemplate", Name = AppRoutes.Templates.SaveRouteTemplate)]
		public JsonResult SaveRouteTemplate(RouteTemplateModel rModel)
		{
			if (!ModelState.IsValid)
				return Json(new { ok = false, message = "Validation has failed!" });

			var routeTemplate = Mapper.Map<RouteTemplate>(rModel);
			_routeTemplateRepository.Update(routeTemplate, routeTemplate.RouteBase);
			_routeTemplateRepository.SaveChanges();

			return Json(new { ok = true });
		}

		[HttpPost, Route("templates/deleteroutetemplate", Name = AppRoutes.Templates.DeleteRouteTemplate)]
		public JsonResult DeleteRouteTemplate(RouteTemplateModel rModel)
		{
		  if (!ModelState.IsValid)
			return Json(new { ok = false, message = "Validation has failed!" });

		  var routeTemplate = Mapper.Map<RouteTemplate>(rModel);
		  _routeTemplateRepository.Remove(routeTemplate, routeTemplate.RouteBase);
		  _routeTemplateRepository.SaveChanges();

		  return Json(new { ok = true });
		}

		[HttpPost, Route("templates/createdaytemplate", Name = AppRoutes.Templates.CreateDayTemplate)]
		public JsonResult CreateDayTemplate(DayTemplateModel rModel)
		{
			if (!ModelState.IsValid)
				return Json(new { ok = false, message = "Validation has failed!" });

			var dayTemplate = Mapper.Map<DayTemplate>(rModel);

			_dayTemplateRepository.Add(dayTemplate);
			dayTemplate.Routes.ToList().ForEach(x => x.DayTemplateId = dayTemplate.Id);
			_dayTemplateRepository.SaveChanges();

			return Json(new { ok = true, templateId = dayTemplate.Id, routeIds = dayTemplate.Routes.OrderBy(x => x.Time).Select(x => x.Id).ToArray() });
		}

		[HttpPost, Route("templates/savedaytemplate", Name = AppRoutes.Templates.SaveDayTemplate)]
		public JsonResult SaveDayTemplate(DayTemplateModel rModel)
		{
			if (!ModelState.IsValid)
				return Json(new { ok = false, message = "Validation has failed!" });

			var dayTemplate = Mapper.Map<DayTemplate>(rModel);
			_dayTemplateRepository.Update(dayTemplate);

			dayTemplate.Routes.ToList().ForEach(x => x.DayTemplateId = dayTemplate.Id);
			var routesToDelete =
				_routeBaseRepository.Get()
					.Where(x => x.DayTemplateId.HasValue && x.DayTemplateId.Value == dayTemplate.Id).ToList().Where(e => dayTemplate.Routes.ToList().All(x => x.Id != e.Id));
			_routeBaseRepository.RemoveRange(routesToDelete);
			_routeBaseRepository.AddOrUpdateRange(dayTemplate.Routes);

			_dayTemplateRepository.SaveChanges();
			_routeBaseRepository.SaveChanges();

			return Json(new { ok = true });
		}

		[HttpPost, Route("templates/deletedaytemplate", Name = AppRoutes.Templates.DeleteDayTemplate)]
		public JsonResult DeleteDayTemplate(DayTemplateModel rModel)
		{
			if (!ModelState.IsValid)
				return Json(new { ok = false, message = "Validation has failed!" });

			var dayTemplate = Mapper.Map<DayTemplate>(rModel);
			_routeBaseRepository.RemoveRange(_routeBaseRepository.Get().Where(x => x.DayTemplateId == dayTemplate.Id));
			_routeBaseRepository.SaveChanges();

			dayTemplate.Routes = null;
			_dayTemplateRepository.Remove(dayTemplate);
			_dayTemplateRepository.SaveChanges();

			return Json(new { ok = true });
		}
	}
}