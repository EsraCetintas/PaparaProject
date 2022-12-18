using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardActivity;
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
        private readonly ICardActivityService _cardActivityService;
        public CardService(ICardRepository cardRepository, ICardActivityService cardActivityService)
        {
            _cardRepository = cardRepository;
            _cardActivityService = cardActivityService;
        }

        public async Task<CardServiceResult> AddAsync(CardCreateDto cardCreateDto)
        {
            var validationResult = CardValidatorManager.CreditCardValidator(cardCreateDto.CreditCard);


            if (!validationResult.Result)
                return new CardServiceResult(false, validationResult.Message);

            if (await CheckExistsCard(cardCreateDto.CreditCard.CardNo))
                return new CardServiceResult(false, "Card Not Found.");

            Card card = new Card();
            card.Balance = cardCreateDto.Balance;
            card.CVV = cardCreateDto.CreditCard.CVV;
            card.CardNo = cardCreateDto.CreditCard.CardNo;
            card.ExpirationDateMonth = cardCreateDto.CreditCard.ExpirationDateMonth;
            card.ExpirationDateYear = cardCreateDto.CreditCard.ExpirationDateYear;
            card.FullName = cardCreateDto.CreditCard.FullName.ToUpper();
            card.UserId = cardCreateDto.UserId;

            await _cardRepository.AddAsync(card);

            return new CardServiceResult(true, "Card Added.");
        }

        public async Task<ObjectId?> FindByCreditCardParams(CreditCardModel creditCardModel)
        {
           var result = await _cardRepository.FindByCardNoAsync(creditCardModel.CardNo);
            if (result.CardNo != creditCardModel.CardNo && result.CVV != creditCardModel.CVV)
                return null;

            if (result.ExpirationDateYear != creditCardModel.ExpirationDateYear && result.ExpirationDateMonth != creditCardModel.ExpirationDateMonth)
                return null;

            if(result.FullName.ToUpper() != creditCardModel.FullName.ToUpper())
                return null;

            return result.Id;
        
        }

        private async Task<bool> CheckExistsCard(string cardNo)
        {
           var result =await _cardRepository.FindByCardNoAsync(cardNo);
            if(result is null)
                return false;
            else return true;
        }

        private async Task<bool> CheckCardBalance(ObjectId creditCardId, decimal amount)
        {
          var card = await _cardRepository.GetCardByIdAsync(creditCardId);
            if(card.Balance<amount)
                return false;
            else return true;
        }

        public async Task<CardServiceResult> ReduceCardBalance(ObjectId creditCardId, decimal amount)
        {
            if (!await CheckCardBalance(creditCardId, amount))
                return new CardServiceResult(false, "Balance of Card is not enough.");
            else
            {
                var card = await _cardRepository.GetCardByIdAsync(creditCardId);

                decimal oldBalance = card.Balance;
                decimal newBalance = card.Balance - amount;

                card.Balance = newBalance;         
                await _cardRepository.UpdateCard(card);

                await this.SaveCardActivity(oldBalance, newBalance, card.Id);

                return new CardServiceResult(true, "Paymnet Successfull.");

            }
        }

        public async Task<CardServiceResult> DeleteAsync(string cardNo)
        {
            if(await CheckExistsCard(cardNo))
            {
               await _cardRepository.DeleteAsync(cardNo);
            return new CardServiceResult(true, "Card Deleted.");
            }
            return new CardServiceResult(false, "Card Not Found.");
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

        private async Task SaveCardActivity(decimal oldBalance,
            decimal newBalance,
            ObjectId cardId)
        {
            CardActivityDto cardActivityDto = new CardActivityDto();
            cardActivityDto.CardId = cardId;
            cardActivityDto.OldBalance = oldBalance;
            cardActivityDto.NewBalance = newBalance;
            cardActivityDto.ActivityDate = DateTime.Now;

            await _cardActivityService.AddAsync(cardActivityDto);
        }
    }
}
