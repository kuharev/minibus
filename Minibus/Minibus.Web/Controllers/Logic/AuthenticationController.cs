using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Minibus.Data.Database;
using Minibus.Data.Model;
using Minibus.Web.Core;
using Minibus.Web.Models.Authentication;

namespace Minibus.Web.Controllers.Logic
{
	public class AuthenticationController : Controller
	{
		private readonly IRepository<User> _userRepository;

		public AuthenticationController(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpGet, Route("authentification/registration", Name = AppRoutes.Authentication.Registration)]
		public PartialViewResult Registration()
		{
			return PartialView();
		}

		[HttpPost, Route("authentification/registration")]
		public JsonResult Registration(RegistrationModel model)
		{
			if (!ModelState.IsValid)
				return Json(new { ok = false, message = "Введите валидные данные" });

			if (GetUser(model.Login) == null)
			{
				var user = model.CreateUser();
				CreateUser(user);
				SignIn(user);
				return Json(new { ok = true, login = model.Login });
			}

			return Json(new { ok = false, message = "Пользователь с таким логином уже существует" });
		}

		[HttpGet, Route("authentification/login", Name = AppRoutes.Authentication.Login)]
		public PartialViewResult Login()
		{
			return PartialView();
		}

		[HttpPost, Route("authentification/login")]
		public JsonResult Login(LoginModel model)
		{

			if (!ModelState.IsValid)
				return Json(new { ok = false, message = "Validation has failed!" });

			var user = GetUser(model.Login);
			if (user != null && user.Password.Equals(model.Password))
			{
				SignIn(user);
				return Json(new { ok = true, login = user.Login });
			}

			return Json(new { ok = false, message = "Login or password is incorrect" });
		}


		[HttpPost, Route("authentication/logout", Name = AppRoutes.Authentication.Logout)]
		public JsonResult Logout()
		{
			HttpContext.GetOwinContext().Authentication.SignOut();

			return Json(new { ok = true });
		}

		#region User actions

		private User GetUser(string login)
		{
			return _userRepository.Get().FirstOrDefault(u => (u.Login.Equals(login) || u.Email.Equals(login)));
		}

		private void CreateUser(User user)
		{
			_userRepository.Add(user);
			_userRepository.SaveChanges();
		}

		private void SignIn(User user)
		{
			var identity = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, user.Login),
					new Claim(ClaimTypes.Email, user.Email)
				}, DefaultAuthenticationTypes.ApplicationCookie);

			HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties
			{
				IsPersistent = false
			}, identity);
		}

		#endregion
	}
}