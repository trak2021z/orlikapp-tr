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
            CreateMap<BusinessLayer.Entities.MatchMember, UserDictionaryItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Player.Name));

            CreateMap<BusinessLayer.Entities.MatchMember, MatchMemberJoinModel>();
        }
    }
}
