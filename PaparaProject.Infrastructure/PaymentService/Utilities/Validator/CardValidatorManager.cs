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
                return new CardCreateValidatorResponseModel { Message = "Kart Numarası Sayısı Geçersiz.", Result = false };

            if(creditCardModel.CVV.Length != 3)
                return new CardCreateValidatorResponseModel { Message = "CVV Sayısı Geçersiz.", Result = false };

            if(creditCardModel.ExpirationDateYear.Length != 4)
                return new CardCreateValidatorResponseModel { Message = "Son kullanma yılını kontrol ediniz.", Result = false };

            return new CardCreateValidatorResponseModel { Message = "Girilen kart bilgileri geçerli.", Result = true };
        }
    }
}
