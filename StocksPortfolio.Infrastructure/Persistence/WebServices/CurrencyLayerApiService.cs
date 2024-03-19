using Newtonsoft.Json.Linq;
using StocksPortfolio.Core.Contracts;
using Microsoft.Extensions.Caching.Memory;
using StocksPortfolio.Infrastructure.Persistence.Contracts;
using StocksPortfolio.Infrastructure.Persistence.WebServices.Abstract;
using StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract;

namespace StocksPortfolio.Infrastructure.Persistence.WebServices
{
    public class CurrencyLayerApiService : ICurrencyRateService
    {
        private const string CacheKey = "CurrencyRatio";

        private readonly MemoryCache _cache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICurrencyLayerApiConfiguration _config;

        public CurrencyLayerApiService(ICurrencyLayerApiConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _cache = new(new MemoryCacheOptions());
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CurrencyRate> GetCurrencyRateAsync(Currencies source, Currencies target)
        {
            var currencyRate = new CurrencyRate();
            currencyRate.SourceCurrency = source;
            currencyRate.TargetCurrency = target;

            if (source == target)
            {
                currencyRate.Rate = 1;
            }
            else
            {
                string key = $"{CacheKey}_{source}";
                var currencyDict = await GetOrAddQuotes(key, source.ToString());
                currencyRate.Rate = currencyDict[$"{source}{target}"];
            }

            return currencyRate;
        }

        private Task<Dictionary<string, decimal>?> GetOrAddQuotes(string key, string source)
        {
            return _cache.GetOrCreateAsync(key, entry => new Lazy<Task<Dictionary<string, decimal>>>(async () =>
            {
                var client = GetHttpClient();
                var response = await client.GetAsync(_config.BaseUrl + $"currency_data/live?source={source}");
                var responseTxt = await response.Content.ReadAsStringAsync();
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<JToken>(responseTxt);

                var quotes = json["quotes"];
                var dict = new Dictionary<string, decimal>();
                foreach (var item in quotes.Children<JProperty>())
                {
                    dict.Add(item.Name, item.Value.Value<decimal>());
                }

                entry.Value = dict;
                entry.AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(1);

                return dict;
            }).Value);
        }

        private HttpClient GetHttpClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("apiKey", _config.ApiKey);

            return client;
        }
    }
}
