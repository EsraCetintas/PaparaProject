using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.DuesDtos
{
    public class DuesUpdateDto
    {
        public int FlatId { get; set; }
        public decimal AmountOfDues { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
