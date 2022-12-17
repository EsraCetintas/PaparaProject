using FluentValidation;
using PaparaProject.Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class RegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(b => b.Name).MaximumLength(100).NotEmpty();
            RuleFor(b => b.SurName).MaximumLength(100).NotEmpty();
            RuleFor(b => b.IdentityNo).MinimumLength(11).MaximumLength(11).NotEmpty();
            RuleFor(b => b.EMail).EmailAddress().MaximumLength(100).NotEmpty();
            RuleFor(b => b.PhoneNumber).MinimumLength(11).MaximumLength(11).NotEmpty();
            RuleFor(b => b.NumberPlate).MaximumLength(20);
            RuleFor(b => b.Password).MinimumLength(6).MaximumLength(20).NotEmpty();
        }
    }
}
