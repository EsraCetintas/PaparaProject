using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<Card> GetCardByIdAsync(ObjectId id);
        Task<Card> FindByCardNoAsync(string cardNo);
        Task AddAsync(Card card);
        Task DeleteAsync(string cardNo);
        Task UpdateCard(Card card);

    }
}
