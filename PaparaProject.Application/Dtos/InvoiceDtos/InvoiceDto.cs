using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaProject.Application.Dtos.FlatDtos;
using PaparaProject.Application.Dtos.InvoiceTypeDtos;

namespace PaparaProject.Application.Dtos.InvoiceDtos
{
    public class InvoiceDto
    {
        public decimal AmountOfInvoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime Deadline { get; set; }

        public FlatDto FlatDto { get; set; }
        public InvoiceTypeDto InvoiceTypeDto { get; set; }
    }
}
