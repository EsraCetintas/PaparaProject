using Autofac.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Concrete.Services;
using PaparaProject.Application.Dtos.RoleDto;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public RolesController(IUserRoleService userRoleService, IRoleService roleService)
        {
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllRoleDtos();
            return Ok(result);
        }

        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(OperationClaimDto operationClaimDto)
        {
            var result = await _roleService.AddAsync(operationClaimDto);
                return Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _roleService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }

        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpPost("roleassign")]
        public async Task<IActionResult> RoleAssign(UserOperationClaimDto userOperationClaimDto)
        {
            var result = await _userRoleService.RoleAssign(userOperationClaimDto);
            return Ok(result);
        }
    }
}
