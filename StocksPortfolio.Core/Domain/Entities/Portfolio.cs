using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StocksPortfolio.Core.Domain.Entities
{
    public class Portfolio : Entity
    { 
        [BsonElement("totalValue")]
        public decimal CurrentTotalValue { get; set; }

        [BsonElement("stocks")]
        public List<Stock> Stocks { get; set; } = new List<Stock>();

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
