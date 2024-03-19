namespace StocksPortfolio.Infrastructure.Persistence.Repositories.Abstract
{
    public interface IAppMongoDbRunner
    {
        public string ConnectionString { get; }
    }
}
