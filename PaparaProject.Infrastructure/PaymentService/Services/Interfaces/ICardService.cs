using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Services.Interfaces
{
    public interface ICardService
    {
        Task<CardServiceResult> AddAsync(CardCreateDto cardCreateDto);

        // Burada kart ID'sini dön.
        Task<ObjectId?> FindByCreditCardParams(CreditCardModel creditCardModel);
        Task<CardServiceResult> ReduceCardBalance(ObjectId creditCardId, decimal amount);
    }
}
