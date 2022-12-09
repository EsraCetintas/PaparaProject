using FluentValidation;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(b => b.Name).MaximumLength(100).NotEmpty();
            RuleFor(b => b.SurName).MaximumLength(100).NotEmpty();
            RuleFor(b => b.IdentityNo).MaximumLength(100).NotEmpty();
            RuleFor(b => b.EMail).MaximumLength(100).NotEmpty();
            RuleFor(b => b.PhoneNumber).MaximumLength(100).NotEmpty();
            RuleFor(b => b.NumberPlate).MaximumLength(20);
        }
    }
}
