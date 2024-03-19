namespace StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract
{
    public interface IMongoDbConfiguration
    {
        public string DatabaseName { get; }
        public string CollectionName { get; }
        public string FileName { get; }
    }
}
