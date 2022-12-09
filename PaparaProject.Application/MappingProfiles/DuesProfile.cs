using AutoMapper;
using PaparaProject.Application.Dtos.DuesDtos;
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
            CreateMap<Dues, DuesDto>().ReverseMap();
            CreateMap<Dues, DuesCreateDto>().ReverseMap();
        }
    }
}
