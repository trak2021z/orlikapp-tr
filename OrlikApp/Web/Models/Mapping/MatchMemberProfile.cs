using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.MatchMember;
using Web.Models.User;

namespace Web.Models.Mapping
{
    public class MatchMemberProfile : Profile
    {
        public MatchMemberProfile()
        {
            CreateMap<BusinessLayer.Entities.MatchMember, UserSimpleItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Player.Name))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName));

            CreateMap<BusinessLayer.Entities.MatchMember, MatchMemberJoinModel>();
        }
    }
}
