using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public int FlatId { get; set; }
        public int InvoiceTypeId { get; set; }
        public decimal AmountOfInvoice { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime Deadline { get; set; }

        public Flat Flat { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}
