using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Dtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.Utilities.Security.JWT;
using PaparaProject.Domain.Entities;

namespace PaparaProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Token))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var userToLogin = _authService.Login(userLoginDto).Result;
            if (userToLogin == null)
            {
                return BadRequest(new APIResult { Message = "", Success = false, Data = null });
            }

            var result = _tokenService.CreateAccessToken(userToLogin);
            return Ok(new APIResult
            {
                Message = "",
                Success = true,
                Data = result
            });
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Token))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userExists = _authService.UserExists(userRegisterDto.EMail);
            if (userExists == null)
            {
                return BadRequest(new APIResult { Message = "", Success = false, Data = null});
            }

            var registerResult = _authService.Register(userRegisterDto).Result;
            var result = _tokenService.CreateAccessToken(registerResult);
            return Ok(new APIResult {
                Message = "",
                Success = true,
                Data= result
            });
        }
    }
}
