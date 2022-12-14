using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Dtos.PaymentDtos;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using PaparaProject.Infrastructure.PaymentService.Utilities.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly ICardService _cardService;
        public PaymentService(ICardService cardService)
        {
            _cardService = cardService;
        }
        public async Task<CardServiceResult> Pay(PaymentPayDto paymentPayDto)
        {
            var validationResult = CardValidatorManager.CreditCardValidator(paymentPayDto.CreditCard);

            if(!validationResult.Result)
                throw new NotImplementedException(); // Validation Hata

            var cardIdResult = await _cardService.FindByCreditCardParams(paymentPayDto.CreditCard);

            if(!cardIdResult.HasValue)
                throw new NotImplementedException(); // Kart bulunamadı.

            var reduceResult = await _cardService.ReduceCardBalance(cardIdResult.Value, paymentPayDto.PaymentAmount);

            return reduceResult;
        }
    }
}
