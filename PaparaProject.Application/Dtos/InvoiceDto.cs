using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos
{
    public class InvoiceDto
    {
        public string HomeOwnerName { get; set; }
        public string HomeOwnerSurName { get; set; }
        public string BlockNo { get; set; }
        public string FloorNo { get; set; }
        public string FlatNo { get; set; }
        public string InvoiceType { get; set; }
        public decimal AmountOfInvoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
