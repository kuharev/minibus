using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Minibus.Data.Database;

namespace Minibus.Web.App_Start
{
	public class IocConfig
	{
		public static void RegisterDependencies()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(Global).Assembly);

			RegisterData(builder);

			IContainer container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}

		public static void RegisterApiDependencies()
		{
			var builder = new ContainerBuilder();
			builder.RegisterApiControllers(typeof(Global).Assembly);

			RegisterData(builder);

			IContainer container = builder.Build();
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}

		private static void RegisterData(ContainerBuilder builder)
		{
			builder.RegisterType(typeof(MinibusContext))
				.As(typeof(MinibusContext))
				.InstancePerRequest();
			builder.RegisterGeneric(typeof (Repository<>))
				.As(typeof (IRepository<>))
				.InstancePerRequest();
		}
	}
}