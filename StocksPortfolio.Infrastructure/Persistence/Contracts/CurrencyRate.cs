using StocksPortfolio.Core.Contracts;

namespace StocksPortfolio.Infrastructure.Persistence.Contracts
{
    public class CurrencyRate
    {
        public Currencies SourceCurrency { get; set; }
        public Currencies TargetCurrency { get; set; }
        public decimal Rate { get; set; }
    }
}
