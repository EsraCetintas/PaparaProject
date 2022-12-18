using AutoMapper;
using PaparaProject.Application.Aspects.Autofac.Validation;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.Utilities.Security.Hashing;
using PaparaProject.Application.Utilities.Security.JWT;
using PaparaProject.Application.ValidationRules.FluentValidation;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(IUserService userService, IMapper mapper, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
        }

        [ValidationAspect(typeof(LoginValidator))]
        public async Task<APIResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _userService.GetByMailAsync(userLoginDto.EMail);
            if (user is null)
                return new APIResult { Success = false, Message = "User Not Found", Data = null };

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                return new APIResult { Success = false, Message = "Password Invalid", Data = null };

            return new APIResult { Success = true, Message = "Login Succesfull", Data = user };
        }

        [ValidationAspect(typeof(RegisterValidator))]

        public async Task<APIResult> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                FlatId = userRegisterDto.FlatId,
                EMail = userRegisterDto.EMail,
                Name = userRegisterDto.Name,
                SurName = userRegisterDto.SurName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IdentityNo = userRegisterDto.IdentityNo,
                PhoneNumber = userRegisterDto.PhoneNumber,
                NumberPlate = userRegisterDto.NumberPlate,
            };
            await _userService.AddAsync(user);
            return new APIResult { Success = true, Message = "Login Succesfull", Data = _mapper.Map<User>(user) };
        }

        public async Task<bool> UserExists(string email)
        {
            var result = await _userService.GetByMailAsync(email);
            if (result is null)
                return false;
            return true;
        }

        public async Task<APIResult> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new APIResult { Success = true, Message = "Token Succesfull", Data = accessToken };
        }

    }
}
