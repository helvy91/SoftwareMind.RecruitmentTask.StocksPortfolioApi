using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StocksPortfolio.Core.Domain.Entities
{
    public class Entity
    {
        [BsonElement("id")]
        public ObjectId? Id { get; set; }
    }
}
