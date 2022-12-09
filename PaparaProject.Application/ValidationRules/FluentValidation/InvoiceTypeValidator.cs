using FluentValidation;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class InvoiceTypeValidator : AbstractValidator<InvoiceType>
    {
        public InvoiceTypeValidator()
        {
            RuleFor(b => b.InvoiceTypeName).MaximumLength(100).NotEmpty();
        }
    }
}
