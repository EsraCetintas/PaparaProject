using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaProject.Application.Dtos.FlatDtos;

namespace PaparaProject.Application.Dtos.DuesDtos
{
    public class DuesDto
    {
        public decimal AmountOfDues { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime Deadline { get; set; }

        public FlatDto Flat { get; set; }
    }
}
