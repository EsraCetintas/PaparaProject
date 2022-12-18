using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.Utilities.Security.JWT;
using PaparaProject.Domain.Entities;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResult))]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userToLogin = await _authService.Login(userLoginDto);
            if (!userToLogin.Success)
            {
                return NotFound(userToLogin);
            }

            else
            {
                var result = _authService.CreateAccessToken((User)userToLogin.Data);
                return Ok(new APIResult
                {
                    Message = userToLogin.Message,
                    Success = true,
                    Data = result
                });
            }
           
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult))]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userExists = _authService.UserExists(userRegisterDto.EMail).Result;
            if (userExists == true)
            {
                return BadRequest(new APIResult { Message = "User Already Registered", Success = false, Data = null});
            }

            else
            {
                var registerResult = _authService.Register(userRegisterDto).Result;
                var result =  _authService.CreateAccessToken((User)registerResult.Data);
                return Ok(new APIResult
                {
                    Message = "Register Successful",
                    Success = true,
                    Data = result
                });
            }
        }

    }
}
