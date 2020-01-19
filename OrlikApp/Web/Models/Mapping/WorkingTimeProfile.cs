using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.WorkingTime;

namespace Web.Models.Mapping
{
    public class WorkingTimeProfile : Profile
    {
        public WorkingTimeProfile()
        {
            CreateMap<WorkingTimeRequest, BusinessLayer.Entities.WorkingTime>();
            CreateMap<BusinessLayer.Entities.WorkingTime, WorkingTimeResponse>()
                .ForMember(dest => dest.OpenHour,
                    opt => opt.MapFrom(src => src.OpenHour.ToString(@"hh\:mm")))
                .ForMember(dest => dest.CloseHour,
                    opt => opt.MapFrom(src => src.CloseHour.ToString(@"hh\:mm")));
        }
    }
}
