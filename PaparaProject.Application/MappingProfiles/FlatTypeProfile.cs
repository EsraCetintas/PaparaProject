using AutoMapper;
using PaparaProject.Application.Dtos.FlatTypeDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.MappingProfiles
{
    public class FlatTypeProfile : Profile
    {
        public FlatTypeProfile()
        {
            CreateMap<FlatType, FlatTypeDto>().ForMember(x=>x.FlatTypeName, 
                opt=>opt.MapFrom(x=>x.FlatTypeName)).ReverseMap();
        }
    }
}
