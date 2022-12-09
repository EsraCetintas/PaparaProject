using AutoMapper;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.Utilities.Security.Hashing;
using PaparaProject.Application.Utilities.Security.JWT;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class AuthService : IAuthService
    {
        readonly IUserService _userService;
        readonly IMapper _mapper;


        public AuthService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<APIResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _userService.GetByMailAsync(userLoginDto.EMail);
            User user = (User)result.Data;
            if (!result.Success)
                return result;

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                return new APIResult { Success = false, Message = "Password Invalid", Data = null };

            return new APIResult {Success = true, Message = "Login Succesfull", Data = _mapper.Map<User>(user) };
        }

        public async Task<APIResult> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                EMail = userRegisterDto.EMail,
                Name = userRegisterDto.Name,
                SurName = userRegisterDto.SurName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            var userDto = _mapper.Map<UserDto>(user);
            await _userService.AddAsync(userDto);
            return new APIResult { Success = true, Message = "Register Succesfull", Data = userDto };
        }

        public async Task<bool> UserExists(string email)
        {
            var result = await _userService.GetByMailAsync(email);
            if (!result.Success)
            {
                return false;
            }
            return true;
        }

    }
}
