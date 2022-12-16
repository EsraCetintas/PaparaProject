using AutoMapper;
using PaparaProject.Application.Dtos.MessageDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.MappingProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>().ForMember(x => x.User, opt => opt.MapFrom(x => x.User)).ReverseMap();
            CreateMap<Message, MessageCreateDto>().ReverseMap();
            CreateMap<Message, MessageUpdateDto>().ReverseMap();
        }
    }
}
