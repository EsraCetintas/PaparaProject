using PaparaProject.Application.Dtos;
using PaparaProject.Application.Interfaces.Services;
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
        readonly ITokenService _tokenHelper;

        public AuthService(IUserService userService, ITokenService tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public async Task<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userLoginDto.EMail);
            if (userToCheck == null)
            {
                throw new Exception("Not Found");
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                throw new Exception("Password Invalid");
            }

            return userToCheck;
        }

        public async Task<User> Register(UserRegisterDto userRegisterDto)
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
            await _userService.AddAsync(user);
            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _userService.GetByMailAsync(email) != null)
            {
                return false;
            }
            return true;
        }

    }
}
