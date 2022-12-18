using PaparaProject.Domain.Entities;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using PaparaProject.Infrastructure.PaymentService.Utilities.Validator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace PaparaProject.Infrastructure.PaymentService.Utilities.Validator
{
    public static class CardValidatorManager
    {
        public static CardCreateValidatorResponseModel CreditCardValidator(CreditCardModel creditCardModel)
        {
            if(creditCardModel.CardNo.Length != 16)
                return new CardCreateValidatorResponseModel { Message = "Card No Invalid.", Result = false };

            if(creditCardModel.CVV.Length != 3)
                return new CardCreateValidatorResponseModel { Message = "CVV Invalid.", Result = false };

            if (creditCardModel.ExpirationDateYear < 2022 && creditCardModel.ExpirationDateYear > 2028)
                return new CardCreateValidatorResponseModel { Message = "Expiration Date Year Invalid.", Result = false };

            if (creditCardModel.ExpirationDateMonth<0 && creditCardModel.ExpirationDateMonth>13)
                return new CardCreateValidatorResponseModel { Message = "Expiration Date Month Invalid", Result = false };
           
            return new CardCreateValidatorResponseModel { Message = "Card Valid.", Result = true };
        }
    }
}
