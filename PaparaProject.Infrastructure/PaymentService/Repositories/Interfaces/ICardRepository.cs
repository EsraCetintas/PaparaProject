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
        Task AddAsync(CardCreateDto card);
        Task UpdateAsync(Card card);
        Task DeleteAsync(Card card);

    }
}
