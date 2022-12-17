using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.CardDto
{
    public class CardCreateDto
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public CreditCardDto CreditCard { get; set; }
    }
}
