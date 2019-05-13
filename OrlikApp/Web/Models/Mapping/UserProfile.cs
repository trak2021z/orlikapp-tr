using AutoMapper;
using BusinessLayer.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.User;

namespace Web.Models.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<BusinessLayer.Entities.User, UserDetailsResponse>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString()))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<BusinessLayer.Entities.User, UserListItem>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<UserSearchRequest, UserSearch>();

            CreateMap<UserCreateRequest, BusinessLayer.Entities.User>();

            CreateMap<UserUpdateRequest, BusinessLayer.Entities.User>();
        }
    }
}
