using AutoMapper;
using BusinessLayer.Models.Auth;
using BusinessLayer.Models.User;
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
            CreateMap<UserException, BadRequestModel>()
                .ForMember(dest => dest.ErrorCode, opt => opt.MapFrom(src => (int)src.ErrorCode));

            CreateMap<AuthException, BadRequestModel>()
                .ForMember(dest => dest.ErrorCode, opt => opt.MapFrom(src => (int)src.ErrorCode));
        }
    }
}
