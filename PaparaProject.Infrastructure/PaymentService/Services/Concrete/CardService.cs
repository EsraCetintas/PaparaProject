using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using PaparaProject.Infrastructure.PaymentService.Model;
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

            //if(!validationResult.Result) // validation
            //    return new CardServiceResult(false, "Kart bakiyesi yetersiz.");

            if (!validationResult.Result) // validation
                return new CardServiceResult(false, validationResult.Message);

            if (!CheckExistsCard(cardCreateDto.CreditCard.CardNo))
                return new CardServiceResult(false, "Kart bulunmaktadır.");

            Card card = new Card();
            card.Balance = cardCreateDto.Balance;
            card.CVV = cardCreateDto.CreditCard.CVV;
            card.CardNo = cardCreateDto.CreditCard.CardNo;
            card.ExpirationDateMonth = cardCreateDto.CreditCard.ExpirationDateMonth;
            card.ExpirationDateYear = cardCreateDto.CreditCard.ExpirationDateYear;
            card.FullName = cardCreateDto.CreditCard.FullName;
            card.UserId = cardCreateDto.UserId;

            await _cardRepository.AddAsync(card);

            return new CardServiceResult(true, "Kart eklendi."); // Result
        }

        // Kartın tüm parametrelerine göre olup olmadğına bak
        // Varsa ID'sini dön.
        public async Task<ObjectId?> FindByCreditCardParams(CreditCardModel creditCardModel)
        {
           var result = await _cardRepository.FindByCardNoAsync(creditCardModel.CardNo);
            if (result.CardNo != creditCardModel.CardNo && result.CVV != creditCardModel.CVV)
                return null;

            if (result.ExpirationDateYear != creditCardModel.ExpirationDateYear && result.ExpirationDateMonth != creditCardModel.ExpirationDateMonth)
                return null;

            if(result.FullName != creditCardModel.FullName)
                return null;

            return result.Id;
        
        }

        // Kredi kartı daha önce kaydedilmiş mi bak. (Sadece kart no)
        private bool CheckExistsCard(string cardNo)
        {
           var result = _cardRepository.FindByCardNoAsync(cardNo);
            if(result is null)
                return false;
            else return true;
        }

        // Kartın bakiyesi var mı yokmu kontrol et.
        private async Task<bool> CheckCardBalance(ObjectId creditCardId, decimal amount)
        {
          var card = await _cardRepository.GetCardByIdAsync(creditCardId);
            if(card.Balance<amount)
                return false;
            else return true;
        }

        // Kart bakiye düşür.
        public async Task<CardServiceResult> ReduceCardBalance(ObjectId creditCardId, decimal amount)
        {
            if (!await CheckCardBalance(creditCardId, amount))
                return new CardServiceResult(false, "Kart bakiyesi yetersiz. Ödeme yapamassınız.");
            else
            {
                var card = await _cardRepository.GetCardByIdAsync(creditCardId);
                card.Balance = card.Balance - amount;
               await _cardRepository.UpdateCard(card);
                return new CardServiceResult(true, "Ödeme işlemi başarılı.");

            }
        }

        public async Task<CardServiceResult> DeleteAsync(string cardNo)
        {
            if(CheckExistsCard(cardNo))
            {
               await _cardRepository.DeleteAsync(cardNo);
            return new CardServiceResult(true, "Kart Silindi.");
            }
            return new CardServiceResult(false, "Kart bulunmamaktadır.");
        }

        public async Task<Card> FindByCardNoAsync(string cardNo)
        {
            var card =await _cardRepository.FindByCardNoAsync(cardNo);
            if (card is null)
            {
                return null;
            }
            return card;
        }
    }
}
