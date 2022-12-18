using PaparaProject.Application.Dtos.RoleDto;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<APIResult> GetAllRoleDtos();
        Task<APIResult> AddAsync(OperationClaimDto operationClaimDto);
        Task<APIResult> DeleteAsync(int id);

    }
}
