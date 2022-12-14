using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Model;
using PaparaProject.Infrastructure.PaymentService.Repositories.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Repositories.Concrete
{
    public class CardRepository : ICardRepository
    {
        readonly IMongoDatabase _database;
        private readonly IMongoCollection<Card> _collection;
        public CardRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoConnection);
            _database = client.GetDatabase(settings.Value.Database);
            _collection = _database.GetCollection<Card>("Cards");
        }

        public async Task AddAsync(CardCreateDto cardCreateDto)
        {
            Card card = new Card();
            await _collection.InsertOneAsync(card);
        }

        public async Task DeleteAsync(Card card)
        {
            await _collection.DeleteOneAsync(c=>c.Id == card.Id);
        }


        public async Task<Card> GetCardByIdAsync(ObjectId id)
        {
            var result = await _collection.FindAsync(c => c.Id == id);
            return (Card)result;
        }

        public async Task UpdateAsync(Card card)
        {
            
        }


    }
}
