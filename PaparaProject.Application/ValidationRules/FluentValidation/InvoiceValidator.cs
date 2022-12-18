using FluentValidation;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.ValidationRules.FluentValidation
{
    public class InvoiceValidator : AbstractValidator<InvoiceCreateDto>
    {
        public InvoiceValidator()
        {
            RuleFor(b => b.AmountOfInvoice).NotEmpty();
            RuleFor(b => b.Deadline).NotEmpty();
            RuleFor(b => b.FlatId).NotEmpty();
            RuleFor(b => b.InvoiceTypeId).NotEmpty();
        }
    }
}
