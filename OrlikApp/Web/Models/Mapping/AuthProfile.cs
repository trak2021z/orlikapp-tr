using AutoMapper;
using BusinessLayer.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Auth;

namespace Web.Models.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterRequest, RegisterModel>();
        }
    }
}
