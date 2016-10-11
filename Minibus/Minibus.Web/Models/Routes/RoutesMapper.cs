using System;
using AutoMapper;
using Minibus.Data.Model;
using Minibus.Web.App_Start;

namespace Minibus.Web.Models.Routes
{
	public class RoutesMapper : IModelMapper
	{
		public void CreateMap()
		{
			RouteBaseModelToRouteBase();
			RouteTemplateModelToRouteTemplate();
			RouteBaseToRouteBaseModel();
		}

		private void RouteBaseModelToRouteBase()
		{
			Mapper.CreateMap<RouteBaseModel, RouteBase>()
				.ForMember(r => r.Id, opt => opt.MapFrom(rm => rm.id))
				.ForMember(r => r.Time, opt => opt.MapFrom(rm => rm.time))
				.ForMember(r => r.From, opt => opt.MapFrom(rm => rm.from))
				.ForMember(r => r.To, opt => opt.MapFrom(rm => rm.to))
				.ForMember(r => r.CarNumber, opt => opt.MapFrom(rm => rm.carNumber))
				.ForMember(r => r.PlaceNumber, opt => opt.MapFrom(rm => rm.placesCount))
				.ForMember(r => r.DriverFullName, opt => opt.MapFrom(rm => rm.driver))
				.ForMember(r => r.DriverPhone, opt => opt.MapFrom(rm => rm.driverPhone))
				.ForMember(r => r.Stops, opt => opt.MapFrom(rm => rm.stops));
		}

		private void RouteBaseToRouteBaseModel()
		{
			Mapper.CreateMap<RouteBase, RouteBaseModel>()
				.ForMember(r => r.id, opt => opt.MapFrom(rm => rm.Id))
				.ForMember(r => r.time, opt => opt.MapFrom(rm => rm.Time))
				.ForMember(r => r.from, opt => opt.MapFrom(rm => rm.From))
				.ForMember(r => r.to, opt => opt.MapFrom(rm => rm.To))
				.ForMember(r => r.carNumber, opt => opt.MapFrom(rm => rm.CarNumber))
				.ForMember(r => r.placesCount, opt => opt.MapFrom(rm => rm.PlaceNumber))
				.ForMember(r => r.driver, opt => opt.MapFrom(rm => rm.DriverFullName))
				.ForMember(r => r.driverPhone, opt => opt.MapFrom(rm => rm.DriverPhone))
				.ForMember(r => r.stops, opt => opt.MapFrom(rm => rm.Stops));
		}

		private void RouteTemplateModelToRouteTemplate()
		{
			Mapper.CreateMap<RouteTemplateModel, RouteTemplate>()
				.ForMember(t => t.Id, opt => opt.MapFrom(tm => tm.templateId))
				.ForMember(t => t.Name, opt => opt.MapFrom(tm => tm.name))
				.ForMember(t => t.RouteBase, opt => opt.MapFrom(tm => (RouteBaseModel) tm));
		}
	}
}