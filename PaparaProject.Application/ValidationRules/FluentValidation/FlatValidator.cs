using FluentValidation;
using PaparaProject.Application.Dtos.FlatDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class FlatValidator : AbstractValidator<FlatCreateDto>
    {
        public FlatValidator()
        {
            RuleFor(b => b.FlatTypeId).NotEmpty();
            RuleFor(b => b.BlockNo).MaximumLength(100).NotEmpty();
            RuleFor(b => b.FloorNo).MaximumLength(100).NotEmpty();
            RuleFor(b => b.FlatNo).NotEmpty();
            RuleFor(b => b.FlatState).NotEmpty();
        }
    }
}
