using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Model
{
    public class Card
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int UserId { get; set; }
        public string CardNo { get; set; }
        public string CVV { get; set; }
        public string ExpirationDateMonth { get; set; }
        public string ExpirationDateYear { get; set; }
        public string FullName { get; set; }
        public decimal Balance { get; set; }
    }
}
