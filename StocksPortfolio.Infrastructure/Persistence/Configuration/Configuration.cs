using Microsoft.Extensions.Configuration;
using StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract;

namespace StocksPortfolio.Infrastructure.Persistence.Configuration
{
    public class AppConfiguration : IMongoDbConfiguration, ICurrencyLayerApiConfiguration
    {
        protected readonly IConfiguration _config;

        public AppConfiguration(IConfiguration config)
        {
            _config = config;
        }

        private string GetValue(string key)
            => _config[key];

        #region MongoDb
        public string DatabaseName
            => GetValue("AppSettings:MongoDb:DatabaseName");
        public string CollectionName
            => GetValue("AppSettings:MongoDb:CollectionName");
        public string FileName
            => GetValue("AppSettings:MongoDb:FileName");
        #endregion

        #region CurrencyLayerApi
        public string BaseUrl 
            => GetValue("AppSettings:CurrencyLayerApi:BaseUrl");
        public string ApiKey 
            => GetValue("AppSettings:CurrencyLayerApi:ApiKey");
        #endregion
    }
}
