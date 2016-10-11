using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using Minibus.Data.Model;

namespace Minibus.Data.Database
{
	public class MinibusContext : DbContext
	{
		public MinibusContext() : base("MinibusContext")
		{}

		public DbSet<User> Users { get; set; }
		public DbSet<RouteBase> RouteBases { get; set; }
		public DbSet<RouteTemplate> RouteTemplates { get; set; }
		public DbSet<Route> Routes { get; set; }
		public DbSet<DayTemplate> DayTemplates { get; set; }
		public DbSet<Day> Days { get; set; }

		public static DbSet<TModel> GetDataStorage<TModel>(MinibusContext context) where TModel : class
		{
			var cType = context.GetType();
			var mType = typeof (DbSet<TModel>);
			var dsProperty = cType.GetProperties().FirstOrDefault(property => property.PropertyType == mType);
			if(dsProperty == null)
				throw new ObjectNotFoundException(string.Format("Data storage for model {0} is not found", mType.FullName));

			return (DbSet<TModel>)dsProperty.GetValue(context);
		}
	}
}
