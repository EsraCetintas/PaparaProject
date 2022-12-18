using FluentValidation;
using PaparaProject.Application.Dtos.MessageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class MessageUpdateValidator : AbstractValidator<MessageUpdateForUserDto>
    {
        public MessageUpdateValidator()
        {
            RuleFor(b => b.Title).MaximumLength(100).NotEmpty();
            RuleFor(b => b.Context).MaximumLength(100).NotEmpty();
        }
    }
}
