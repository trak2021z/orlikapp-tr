﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Field;

namespace Web.Models.Mapping
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<BusinessLayer.Entities.Field, FieldItem>()
                .ForMember(dest => dest.KeeperName, opt => opt.MapFrom(src => src.Keeper.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name));
        }
    }
}
