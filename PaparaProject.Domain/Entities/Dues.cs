using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Domain.Entities
{
    public class Dues : BaseEntity
    {
        public int FlatId { get; set; }
        public decimal AmountOfDues { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? Deadline { get; set; }

        public Flat Flat { get; set; }
    }
}
