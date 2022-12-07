using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos
{
    public class DuesDto
    {
        public string FlatNo { get; set; }
        public string BlockNo { get; set; }
        public decimal AmountOfDues { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
