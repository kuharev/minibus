using System;
using System.Linq;
using System.Web.Http;
using Minibus.Data.Database;
using Minibus.Data.Model;
using Minibus.Web.Core;
using Minibus.Web.Models.Days;

namespace Minibus.Web.Controllers.API
{
    public class ScheduleController : ApiController
    {
			private readonly IRepository<DayTemplate> _dayTemplateRepository;
			private readonly IRepository<Day> _dayRepository;

			public ScheduleController(IRepository<DayTemplate> dayTemplateRepository,
				IRepository<Day> dayRepository)
			{
				_dayTemplateRepository = dayTemplateRepository;
				_dayRepository = dayRepository;
			}

			[HttpGet, Route("api/schedule/day", Name = AppRoutes.ScheduleApi.ScheduleDay)]
			public DayModel SheduleDay()
			{
				throw new NotImplementedException();
			}
	}
}