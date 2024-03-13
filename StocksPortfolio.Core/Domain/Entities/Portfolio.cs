using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StocksPortfolio.Core.Domain.Entities
{
    public class Portfolio
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("totalValue")]
        public float CurrentTotalValue { get; set; }

        [BsonElement("stocks")]
        public ICollection<Stock> Stocks { get; set; }
    }
}
