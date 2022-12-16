using AutoMapper;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Dtos.FlatDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.MappingProfiles
{
    public class DuesProfile : Profile
    {
        public DuesProfile()
        {
            CreateMap<Dues, DuesDto>().ForMember(x => x.Flat, opt => opt.MapFrom(x => x.Flat)).ReverseMap();
            CreateMap<Dues, DuesCreateDto>().ReverseMap();
            CreateMap<Dues, DuesUpdateDto>().ReverseMap();

        }
    }
}
