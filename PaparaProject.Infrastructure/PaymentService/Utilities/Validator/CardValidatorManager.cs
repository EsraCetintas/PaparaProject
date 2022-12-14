using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using PaparaProject.Infrastructure.PaymentService.Utilities.Validator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Utilities.Validator
{
    public static class CardValidatorManager
    {
        public static CardCreateValidatorResponseModel CreditCardValidator(CreditCardModel creditCardModel)
        {

            return new CardCreateValidatorResponseModel();
        }
    }
}
