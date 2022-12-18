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
        public CardRepository()
        {
            var settings = MongoClientSettings.FromConnectionString(MongoSettings.MongoConnection);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            _database = client.GetDatabase(MongoSettings.Database);
            _collection = _database.GetCollection<Card>("Cards");
        }

        public async Task AddAsync(Card card)
        {
            await _collection.InsertOneAsync(card);
        }

        public async Task DeleteAsync(string cardNo)
        {
            var filter = Builders<Card>.Filter.Eq(c => c.CardNo, cardNo);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<Card> FindByCardNoAsync(string cardNo)
        {
            var card = _collection.Find(c => c.CardNo == cardNo).FirstOrDefault();
            return  (Card)card;
           
        }

        public async Task<Card> GetCardByIdAsync(ObjectId id)
        {
            var card = _collection.Find(c => c.Id == id).FirstOrDefault();
            return (Card)card;
        }

        public async Task UpdateCard(Card card)
        {
            var filter = Builders<Card>.Filter.Eq(c => c.Id, card.Id);
            var cardToUpdate = Builders<Card>.Update.Set(c=>c.Balance, card.Balance);
           await _collection.UpdateOneAsync(filter, cardToUpdate);
        }
    }
}
