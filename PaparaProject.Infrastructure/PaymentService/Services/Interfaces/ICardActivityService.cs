using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardActivity;
using PaparaProject.Infrastructure.PaymentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Services.Interfaces
{
    public interface ICardActivityService
    {
        Task AddAsync(CardActivityDto cardActivityDto);

        Task<List<CardActivity>> GetCardByIdAsync(ObjectId id);

    }
}
