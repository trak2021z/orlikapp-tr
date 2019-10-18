using AutoMapper;
using BusinessLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Helpers;

namespace Web.Models.Mapping
{
    public class ExceptionProfile : Profile
    {
        public ExceptionProfile()
        {
            CreateMap<BusinessLogicException, BadRequestModel>();
        }
    }
}
