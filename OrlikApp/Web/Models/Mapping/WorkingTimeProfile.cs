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
        }
    }
}
