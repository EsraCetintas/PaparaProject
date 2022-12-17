using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Model
{
    public class CardActivity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId CardId { get; set; }
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
