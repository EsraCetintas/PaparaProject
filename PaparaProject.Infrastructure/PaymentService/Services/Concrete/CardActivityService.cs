using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardActivity;
using PaparaProject.Infrastructure.PaymentService.Model;
using PaparaProject.Infrastructure.PaymentService.Repositories.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Services.Concrete
{
    public class CardActivityService : ICardActivityService
    {
        private readonly ICardActivityRepository _cardActivityRepository;

        public CardActivityService(ICardActivityRepository cardActivityRepository)
        {
            _cardActivityRepository = cardActivityRepository;
        }

        public async Task AddAsync(CardActivityDto cardActivityDto)
        {
            CardActivity cardActivity = new CardActivity();
            cardActivity.CardId = cardActivityDto.CardId;
            cardActivity.ActivityDate = DateTime.Now;
            cardActivity.OldBalance = cardActivityDto.OldBalance;
            cardActivity.NewBalance = cardActivityDto.NewBalance;
            await _cardActivityRepository.AddAsync(cardActivity);  
        }

        public async Task<List<CardActivity>> GetCardByIdAsync(ObjectId id)
        {
           return await _cardActivityRepository.GetCardByIdAsync(id);
        }
    }
}
