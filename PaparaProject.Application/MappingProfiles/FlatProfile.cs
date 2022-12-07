using AutoMapper;
using PaparaProject.Application.Dtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.MappingProfiles
{
    public class FlatProfile : Profile
    {
        public FlatProfile()
        {
            CreateMap<Flat, FlatDto>().ReverseMap();
        }
    }
}
