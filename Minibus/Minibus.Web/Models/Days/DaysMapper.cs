
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Minibus.Data.Model;
using Minibus.Web.App_Start;
using Minibus.Web.Models.Routes;

namespace Minibus.Web.Models.Days
{
	public class DaysMapper : IModelMapper
	{
		public void CreateMap()
		{
			DayTemplateModelToDayTemplate();
			DayTemplateToDayTemplateModel();
			DayToDayModel();
		    DayModelToDay();
		}

		private void DayToDayModel()
		{
			Mapper.CreateMap<Day, DayModel>()
				.ForMember(t => t.id, opt => opt.MapFrom(tm => tm.Id))
				.ForMember(t => t.date, opt => opt.MapFrom(tm => tm.Date))
				.ForMember(t => t.routes, opt => opt.MapFrom(tm => Mapper.Map<List<RouteBase>, List<RouteBaseModel>>(tm.Routes.ToList())));
		}

        private void DayModelToDay()
        {
            Mapper.CreateMap<DayModel, Day>()
                .ForMember(t => t.Id, opt => opt.MapFrom(tm => tm.id))
                .ForMember(t => t.Date, opt => opt.MapFrom(tm => DateTime.ParseExact(tm.date, "dd.MM.yyyy", CultureInfo.InvariantCulture)))
                .ForMember(t => t.Routes, opt => opt.MapFrom(tm => Mapper.Map<List<RouteBaseModel>, List<RouteBase>>(tm.routes.ToList())));
        }

		private void DayTemplateModelToDayTemplate()
		{
			Mapper.CreateMap<DayTemplateModel, DayTemplate>()
				.ForMember(t => t.Id, opt => opt.MapFrom(tm => tm.templateId))
				.ForMember(t => t.Name, opt => opt.MapFrom(tm => tm.name))
				.ForMember(t => t.Routes, opt => opt.MapFrom(tm => Mapper.Map<List<RouteBaseModel>, List<RouteBase>>(tm.routes)));
		}

		private void DayTemplateToDayTemplateModel()
		{
			Mapper.CreateMap<DayTemplate, DayTemplateModel>()
				.ForMember(t => t.templateId, opt => opt.MapFrom(tm => tm.Id))
				.ForMember(t => t.name, opt => opt.MapFrom(tm => tm.Name))
				.ForMember(t => t.routes, opt => opt.MapFrom(tm => Mapper.Map<List<RouteBase>, List<RouteBaseModel>>(tm.Routes.ToList())));
		}
	}
}