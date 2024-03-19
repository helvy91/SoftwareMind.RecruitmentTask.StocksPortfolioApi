using Mongo2Go;
using StocksPortfolio.Infrastructure.Persistence.Repositories.Abstract;
using StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract;

namespace StocksPortfolio.Infrastructure.Persistence.Repositories
{
    public class AppMongoDbRunner : IAppMongoDbRunner, IDisposable
    {
        private readonly MongoDbRunner _runner;

        public string ConnectionString
            => _runner.ConnectionString; 
        public AppMongoDbRunner(IMongoDbConfiguration config)
        {
            _runner = MongoDbRunner.Start();
            _runner.Import(config.DatabaseName, config.CollectionName, Path.Combine("Data", config.FileName), true);
        }

        public void Dispose()
        {
            _runner.Dispose();
        }
    }
}
