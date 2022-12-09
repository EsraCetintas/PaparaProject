﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.InvoiceDtos
{
    public class InvoiceCreateDto
    {
        public int FlatId { get; set; }
        public int InvoiceTypeId { get; set; }
        public decimal AmountOfInvoice { get; set; }
        public DateTime Deadline { get; set; }
    }
}
