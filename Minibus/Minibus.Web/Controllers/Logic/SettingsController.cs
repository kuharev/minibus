using System.Linq;
using System.Web.Mvc;
using Minibus.Data.Database;
using Minibus.Data.Model;
using Minibus.Web.Core;

namespace Minibus.Web.Controllers.Logic
{
	public class SettingsController : Controller
	{
		private readonly IRepository<User> _userRepository;

		public SettingsController(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpGet, Route("settings/usersettings", Name = AppRoutes.Settings.User)]
		public PartialViewResult UserSettings()
		{
			return PartialView(GetUserById(1));
		}

		[HttpPost, Route("settings/usersettings")]
		public JsonResult UserSettings(User rModel)
		{
			if (!ModelState.IsValid)
				return Json(new { ok = false });

			_userRepository.Update(rModel);
			_userRepository.SaveChanges();

			return Json(new { ok = true });
		}

		[HttpGet, Route("settings/operatorsettings", Name = AppRoutes.Settings.Operator)]
		public PartialViewResult OperatorSettings()
		{
			return PartialView(GetUserById(1));
		}

		[HttpPost, Route("settings/operatorsettings")]
		public JsonResult OperatorSettings(User rModel)
		{
			if (!ModelState.IsValid)
				return Json(new { ok = false });

			_userRepository.Update(rModel);
			_userRepository.SaveChanges();

			return Json(new { ok = true });
		}

		private User GetUserById(int id)
		{
			return _userRepository.Get().FirstOrDefault(x => x.Id == id);
		}
	}
}