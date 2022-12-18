using FluentValidation;
using PaparaProject.Application.Dtos.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class UserRoleValidator : AbstractValidator<UserOperationClaimDto>
    {
        public UserRoleValidator()
        {
            RuleFor(b => b.UserId).NotEmpty();
            RuleFor(b => b.OperationClaimId).NotEmpty();
        }
    }
}
