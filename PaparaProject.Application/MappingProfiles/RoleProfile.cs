using AutoMapper;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Dtos.RoleDto;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
        }
    }
}
