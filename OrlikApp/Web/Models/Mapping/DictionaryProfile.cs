using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Helpers;

namespace Web.Models.Mapping
{
    public class DictionaryProfile : Profile
    {
        public DictionaryProfile()
        {
            CreateMap<BusinessLayer.Entities.Role, DictionaryModel>();
            CreateMap<BusinessLayer.Entities.FieldType, DictionaryModel>();
        }
    }
}
