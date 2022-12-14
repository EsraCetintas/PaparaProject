using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos
{
    public class CardCreateDto
    {
        public int UserId { get; set; }      
        public decimal Balance { get; set; }
        public CreditCardModel CreditCard { get; set; }
    }
}
