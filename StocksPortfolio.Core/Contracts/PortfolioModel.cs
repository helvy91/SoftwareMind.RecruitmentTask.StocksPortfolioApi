using MongoDB.Bson;

namespace StocksPortfolio.Core.Contracts
{
    public class PortfolioModel
    {
        public ObjectId? Id { get; set; }
        public decimal CurrentTotalValue { get; set; }
        public List<StockModel> Stocks { get; set; } = new List<StockModel>();
        public bool IsDeleted { get; set; }
    }
}
