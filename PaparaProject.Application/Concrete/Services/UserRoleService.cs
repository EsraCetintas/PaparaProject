using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Aspects.Autofac.Validation;
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
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;

        public UserRoleService(IUserRoleRepository repository)
        {
            _repository = repository;
        }
        [ValidationAspect(typeof(UserRoleValidator))]
        public async Task<APIResult> RoleAssign(UserOperationClaimDto userOperationClaimDto)
        {
            UserOperationClaim userOperationClaim = new UserOperationClaim();
            userOperationClaim.UserId = userOperationClaimDto.UserId;
            userOperationClaim.OperationClaimId = userOperationClaimDto.OperationClaimId;

            await _repository.AddAsync(userOperationClaim);

            return new APIResult { Success= true, Message = "Role Assigned", Data = null };
        }
    }
}
