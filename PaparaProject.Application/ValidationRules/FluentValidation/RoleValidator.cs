using FluentValidation;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Dtos.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class RoleValidator : AbstractValidator<OperationClaimDto>
    {
        public RoleValidator()
        {
            RuleFor(b => b.RoleName).MaximumLength(100).NotEmpty();
        }
    }
}
