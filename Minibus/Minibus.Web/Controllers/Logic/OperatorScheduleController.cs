
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Minibus.Data.Database;
using Minibus.Data.Model;
using Minibus.Web.Core;
using Minibus.Web.Models.Days;

namespace Minibus.Web.Controllers.Logic
{
	public class OperatorScheduleController : Controller
	{
        private readonly IRepository<Day> _dayRepository;
        private readonly IRepository<RouteBase> _routeBaseRepository;

        public OperatorScheduleController(IRepository<Day> dayRepository, IRepository<RouteBase> routeBaseRepository)
		{
			_dayRepository = dayRepository;
            _routeBaseRepository = routeBaseRepository;
		}

		[HttpGet, Route("operatorschedule/index", Name = AppRoutes.OperatorSchedule.Index)]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost, Route("operatorschedule/getday", Name = AppRoutes.OperatorSchedule.GetDay)]
		public JsonResult SheduleGetDay(string date, string from, string to)
		{
            var dateInDateTime = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var day = _dayRepository.Get().FirstOrDefault(x => x.Date == dateInDateTime);
            var dayShedule = Mapper.Map<DayModel>(day);

            dayShedule = dayShedule ?? new DayModel() { id = 0 };
            return Json(new { ok = true, dayShedule });
		}

		[HttpPost, Route("operatorschedule/savetemplate", Name = AppRoutes.OperatorSchedule.SaveTemplate)]
        public JsonResult SaveTemplate(DayModel rModel)
		{
            if (!ModelState.IsValid)
                return Json(new { ok = false, message = "Validation has failed!" });

            var dayShedule = Mapper.Map<Day>(rModel);
            _dayRepository.Update(dayShedule);

            dayShedule.Routes.ToList().ForEach(x => x.DayId = dayShedule.Id);
            
            _dayRepository.SaveChanges();

		    if (dayShedule.Id != 0)
		    {
                var routesToDelete =
                _routeBaseRepository.Get()
                    .Where(x => x.DayId.HasValue && x.DayId.Value == dayShedule.Id).ToList().Where(e => dayShedule.Routes.ToList().All(x => x.Id != e.Id));
                _routeBaseRepository.RemoveRange(routesToDelete);
                _routeBaseRepository.AddOrUpdateRange(dayShedule.Routes);

                _routeBaseRepository.SaveChanges();
		    }

			return Json(new { ok = true });
		}
	}
}