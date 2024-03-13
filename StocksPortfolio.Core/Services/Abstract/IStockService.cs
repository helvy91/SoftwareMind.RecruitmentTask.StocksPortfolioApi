using StocksPortfolio.Core.Contracts;

namespace StocksPortfolio.Core.Services.Abstract
{
    public interface IStocksService
    {
        Task<StockModel> GetStockPrice(string ticker);
    }
}
