using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using PaparaProject.Infrastructure.PaymentService.Repositories.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using PaparaProject.Infrastructure.PaymentService.Utilities.Validator;

namespace PaparaProject.Infrastructure.PaymentService.Services.Concrete
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<CardServiceResult> AddAsync(CardCreateDto cardCreateDto)
        {
            var validationResult = CardValidatorManager.CreditCardValidator(cardCreateDto.CreditCard);

            if(!validationResult.Result) // validation
                return new CardServiceResult(false, "Kart bakiyesi yetersiz.");

            if (!CheckExistsCard(cardCreateDto))
                throw new NotImplementedException(); // Hata

            await _cardRepository.AddAsync(cardCreateDto);

            return new CardServiceResult(true, "Kart eklendi."); // Result
        }

        // Kartın tüm parametrelerine göre olup olmadğına bak
        // Varsa ID'sini dön.
        public async Task<ObjectId?> FindByCreditCardParams(CreditCardModel creditCardModel)
        {
            return null;
        }

        // Kredi kartı daha önce kaydedilmiş mi bak. (Sadece kart no)
        private bool CheckExistsCard(CardCreateDto cardCreateDto)
        {
            return true;
        }

        // Kartın bakiyesi var mı yokmu kontrol et.
        private Task<bool> CheckCardBalance(ObjectId creditCardId)
        {
            throw new NotImplementedException();
        }

        // Kart bakiye düşür.
        public async Task<CardServiceResult> ReduceCardBalance(ObjectId creditCardId)
        {
            if (await CheckCardBalance(creditCardId))
                return new CardServiceResult(false, "Kart bakiyesi yetersiz.");
            
            var creditCard = _cardRepository.GetCardByIdAsync(creditCardId);

            return new CardServiceResult(true, "Ödeme işlemi başarılı.");
        }
    }
}
