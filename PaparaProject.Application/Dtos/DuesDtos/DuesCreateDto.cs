using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.DuesDtos
{
    public class DuesCreateDto
    {
        public int FlatId { get; set; }
        public int CreatedBy { get; set; }
        public decimal AmountOfDues { get; set; }
        public DateTime Deadline { get; set; }
    }
}
