using System;
using System.Linq;
using Autofac;

namespace Minibus.Web.App_Start
{
	internal static class MapperInitialization
	{
		internal static void CreateMaps()
		{
			var mappers = typeof (Global).Assembly
				.GetTypes()
				.Where(IsMapper)
				.Select(CreateMapper);
			foreach (var mapper in mappers)
				mapper.CreateMap();
		}

		private static bool IsMapper(Type t)
		{
			return t.IsAssignableTo<IModelMapper>() && t.GetConstructor(Type.EmptyTypes) != null;
		}

		private static IModelMapper CreateMapper(Type mapperType)
		{
				return (IModelMapper)Activator.CreateInstance(mapperType);
		}
	}

	internal interface IModelMapper
	{
		void CreateMap();
	}
}