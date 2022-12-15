using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.CardDto
{
    public class CardDto
    {
        public decimal? PaymentAmount { get; set; }
        public CreditCardDto CreditCard { get; set; }
    }
}
