namespace StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract
{
    public interface ICurrencyLayerApiConfiguration
    {
        public string BaseUrl { get; }
        public string ApiKey { get; }
    }
}
