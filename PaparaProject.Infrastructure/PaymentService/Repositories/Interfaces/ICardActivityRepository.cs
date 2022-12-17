using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Repositories.Interfaces
{
    public interface ICardActivityRepository
    {
        Task AddAsync(CardActivity cardActivity);
        Task<List<CardActivity>> GetCardByIdAsync(ObjectId id);

    }
}
