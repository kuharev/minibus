using System.Web.Mvc;
using Minibus.Web.Core;

namespace Minibus.Web.Controllers.Logic
{
	public class HomeController : Controller
	{
		[HttpGet]
		[Route("~/")]
		[Route("home/index", Name = AppRoutes.Home.Index)]
		public ActionResult Index()
		{
			return View("Layout");
		}

		[HttpGet, Route("home/getuser", Name = AppRoutes.Home.User)]
		public PartialViewResult GetUser()
		{
			return PartialView();
		}

		[HttpGet, Route("home/operator", Name = AppRoutes.Home.Operator)]
		public PartialViewResult Operator()
		{
			return PartialView();
		}
	}
}