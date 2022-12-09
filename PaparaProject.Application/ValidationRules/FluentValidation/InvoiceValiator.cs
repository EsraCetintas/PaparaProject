using FluentValidation;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class InvoiceValiator : AbstractValidator<Invoice>
    {
        public InvoiceValiator()
        {
            RuleFor(b => b.AmountOfInvoice).NotEmpty();
            RuleFor(b => b.Deadline).NotEmpty();
            RuleFor(b => b.FlatId).NotEmpty();
            RuleFor(b => b.InvoiceTypeId).NotEmpty();
        }
    }
}
