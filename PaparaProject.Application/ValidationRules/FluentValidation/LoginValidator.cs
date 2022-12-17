using FluentValidation;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class LoginValidator : AbstractValidator<UserLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(b => b.EMail).EmailAddress().MaximumLength(100).NotEmpty();
            RuleFor(b => b.Password).MinimumLength(6).MaximumLength(20).NotEmpty();
        }
    }
}
