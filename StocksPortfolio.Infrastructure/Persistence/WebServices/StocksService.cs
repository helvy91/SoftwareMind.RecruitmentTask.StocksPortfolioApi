using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Infrastructure.Persistence.Contracts;
using StocksPortfolio.Infrastructure.Persistence.WebServices.Abstract;

namespace StocksPortfolio.Infrastructure.Persistence.WebServices
{
    public class StocksService : IStocksService
    {
        private readonly Currencies[] _currencies = [Currencies.PLN, Currencies.EUR, Currencies.USD, Currencies.JPY, Currencies.GBP];

        public async Task<StockPrice> GetStockPriceAsync(string ticker)
        {
            var random = new Random();
            return await Task.FromResult(new StockPrice
            {
                Price = random.Next(10, 1000),
                Currency = _currencies[random.Next(0, _currencies.Length)],
            });
        }
    }
}
