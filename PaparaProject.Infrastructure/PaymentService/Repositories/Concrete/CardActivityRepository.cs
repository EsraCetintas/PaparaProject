using MongoDB.Bson;
using MongoDB.Driver;
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
    public class CardActivityRepository : ICardActivityRepository
    {
        readonly IMongoDatabase _database;
        private readonly IMongoCollection<CardActivity> _collection;
        public CardActivityRepository()
        {
            var settings = MongoClientSettings.FromConnectionString(MongoSettings.MongoConnection);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            _database = client.GetDatabase(MongoSettings.Database);
            _collection = _database.GetCollection<CardActivity>("CardActivities");
        }

        public async Task AddAsync(CardActivity cardActivity)
        {
            await _collection.InsertOneAsync(cardActivity);
        }

        public async Task<List<CardActivity>> GetCardByIdAsync(ObjectId id)
        {
            var cards = _collection.Find(c => c.CardId == id).ToList();
            return cards;
        }
    }
}
