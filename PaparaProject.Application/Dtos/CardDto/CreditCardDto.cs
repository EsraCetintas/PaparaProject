using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.CardDto
{
    public class CreditCardDto
    {
        public string CardNo { get; set; }
        public string CVV { get; set; }
        public string ExpirationDateMonth { get; set; }
        public string ExpirationDateYear { get; set; }
        public string FullName { get; set; }
    }
}
