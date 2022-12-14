using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Dtos.PaymentDtos
{
    public class PaymentPayDto
    {
        public decimal PaymentAmount { get; set; }
        public CreditCardModel CreditCard { get; set; }
    }
}
