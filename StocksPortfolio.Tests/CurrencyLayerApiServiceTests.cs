using Moq;
using RichardSzalay.MockHttp;
using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Infrastructure.Persistence.WebServices;
using StocksPortfolio.Infrastructure.Persistence.Configuration.Abstract;

namespace StocksPortfolio.Tests
{
    [TestClass]
    public class CurrencyLayerApiServiceTests
    {
        [TestMethod]
        public async Task  Get_EUR_rate_for_USD()
        {
            //ARRANGE
            var config = new Mock<ICurrencyLayerApiConfiguration>();
            config.Setup(x => x.ApiKey).Returns("testKey");
            config.Setup(x => x.BaseUrl).Returns("http://dummy.com/");
            var json = @"{
                'success': true,
                'timestamp': 1710694503,
                'source': 'USD',
                'quotes': {
                    'USDEUR': 0.91755,
                }
            }";
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(config.Object.BaseUrl + "currency_data/live?source=USD")
                    .Respond("application/json", json);
            var client = new HttpClient(mockHttp);
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(x => x.CreateClient(string.Empty)).Returns(client).Verifiable();
            var service = new CurrencyLayerApiService(config.Object, factory.Object);

            //ACT
            var rate = await service.GetCurrencyRateAsync(Currencies.USD, Currencies.EUR);

            //ASSERT
            Assert.AreEqual(rate.SourceCurrency, Currencies.USD);
            Assert.AreEqual(rate.TargetCurrency, Currencies.EUR);
            Assert.AreEqual(rate.Rate, 0.91755M);
        }
    }
}