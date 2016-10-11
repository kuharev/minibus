using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Minibus.Data.Database;
using Minibus.Data.Migrations;
using Minibus.Web.App_Start;

namespace Minibus.Web
{
	public class Global : HttpApplication
	{
		void Application_Start(object sender, EventArgs e)
		{
			IocConfig.RegisterDependencies();
			IocConfig.RegisterApiDependencies();
			DataBaseStartUp();

			MapperInitialization.CreateMaps();

			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		public static void DataBaseStartUp()
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<MinibusContext, Configuration>());

			using (var context = new MinibusContext())
			{
				context.Database.Initialize(true);
			}
		}
	}
}