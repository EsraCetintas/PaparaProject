using AutoMapper;
using PaparaProject.Application.Dtos.InvoiceTypeDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.MappingProfiles
{
    public class InvoiceTypeProfile : Profile
    {
        public InvoiceTypeProfile()
        {
            CreateMap<InvoiceType, InvoiceTypeDto>().ReverseMap();
        }
    }
}
