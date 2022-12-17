using FluentValidation;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class DuesValidator : AbstractValidator<DuesCreateDto>
    {
        public DuesValidator()
        {
            RuleFor(b => b.AmountOfDues).NotEmpty();
            RuleFor(b => b.Deadline).NotEmpty();
            RuleFor(b => b.FlatId).NotEmpty();
        }
    }
}
