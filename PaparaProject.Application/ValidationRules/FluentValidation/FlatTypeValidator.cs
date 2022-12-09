using FluentValidation;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class FlatTypeValidator : AbstractValidator<FlatType>
    {
        public FlatTypeValidator()
        {
            RuleFor(b => b.FlatTypeName).MaximumLength(100).NotEmpty();

        }
    }
}
