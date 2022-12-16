using AutoMapper;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.MappingProfiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(x=>x.Flat, opt=>opt.MapFrom(x=>x.Flat)).ReverseMap();
            CreateMap<Invoice, InvoiceCreateDto>().ReverseMap();
            CreateMap<Invoice, InvoiceUpdateDto>().ReverseMap();

        }
    }
}
