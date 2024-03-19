using StocksPortfolio.Core.Contracts;

namespace StocksPortfolio.Infrastructure.Persistence.Contracts
{
    public class StockPrice
    {
        public decimal Price { get; set; }
        public Currencies Currency { get; set; }
    }
}
