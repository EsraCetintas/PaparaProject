using AutoMapper;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Aspects.Autofac.Validation;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Dtos.RoleDto;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.ValidationRules.FluentValidation;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [SecuredOperationAspect("Admin")]
        [ValidationAspect(typeof(RoleValidator))]
        public async Task<APIResult> AddAsync(OperationClaimDto operationClaimDto)
        {
            OperationClaim operationClaim = new OperationClaim();
            operationClaim.RoleName = operationClaimDto.RoleName;
            await _repository.AddAsync(operationClaim);
            return new APIResult { Success = true, Message = "Role Added", Data = null };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var operationClaimToDelete = await _repository.GetAsync(x => x.Id == id);
            if (operationClaimToDelete is null)
                return new APIResult { Success = false, Message = "Role Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(operationClaimToDelete);
                return new APIResult { Success = true, Message = "Deleted Operation Claim", Data = null };
            }
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetAllRoleDtos()
        {
           var roles = await _repository.GetAllAsync();
            var roleDtos = _mapper.Map<List<OperationClaimDto>>(roles);
            return new APIResult { Success = true, Message = "All Roles Brought", Data = roleDtos };
        }
    }
}
