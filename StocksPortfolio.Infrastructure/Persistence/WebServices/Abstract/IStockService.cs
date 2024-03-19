using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Infrastructure.Persistence.Contracts;
namespace StocksPortfolio.Infrastructure.Persistence.WebServices.Abstract
{
    public interface IStocksService
    {
        Task<StockPrice> GetStockPriceAsync(string ticker);
    }
}
