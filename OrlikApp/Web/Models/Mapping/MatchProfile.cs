using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Match;

namespace Web.Models.Mapping
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<MatchRequest, BusinessLayer.Entities.Match>();

            CreateMap<BusinessLayer.Entities.Match, MatchItem>()
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => src.StartDate.ToString("yyyy/MM/dd H:mm")))
                .ForMember(dest => dest.EndOfJoiningDate,
                    opt => opt.MapFrom(src => src.EndOfJoiningDate.ToString("yyyy/MM/dd H:mm")));

            CreateMap<BusinessLayer.Entities.Match, MatchCreateResponse>();
        }
    }
}
