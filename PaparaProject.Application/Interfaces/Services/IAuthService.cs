﻿using PaparaProject.Application.Dtos;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<APIResult> Register(UserRegisterDto userRegisterDto);
        Task<APIResult> Login(UserLoginDto userLoginDto);
        Task<bool> UserExists(string email);

    }
}
