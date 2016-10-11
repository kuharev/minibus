using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Minibus.Web.App_Start;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(AuthenticationStartup))]
namespace Minibus.Web.App_Start
{
	public class AuthenticationStartup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuthentication(app);
		}

		public void ConfigureAuthentication(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
			});
		}
	}
}