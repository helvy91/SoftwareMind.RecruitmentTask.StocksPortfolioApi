using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Infrastructure.Persistence.Contracts;

namespace StocksPortfolio.Infrastructure.Persistence.WebServices.Abstract
{
    public interface ICurrencyRateService
    {
        Task<CurrencyRate> GetCurrencyRateAsync(Currencies source, Currencies target);
    }
}
