using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using StocksPortfolio.Core.Contracts;
using StocksPortfolio.Core.Services.Abstract;
using StocksPortfolio.Infrastructure.Persistence.Contracts;
using StocksPortfolio.Infrastructure.Persistence.WebServices.Abstract;

namespace StocksPortfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IStocksService _stocksService;
        private readonly IEntityService<PortfolioModel> _service;
        private readonly ICurrencyRateService _currencyRateService;

        public PortfolioController(IEntityService<PortfolioModel> service, 
                                   IStocksService stocksService, 
                                   ICurrencyRateService currencyRateService)
        {
            _service = service;
            _stocksService = stocksService;
            _currencyRateService = currencyRateService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!ObjectId.TryParse(id, out var value)) 
            {
                return BadRequest();
            }
            
            var portfolio = await _service.GetAsync(value);
            if (portfolio == null)
            {
                return NotFound();
            }

            return Ok(portfolio);
        }

        [HttpGet("value")]
        public async Task<IActionResult> GetTotalPortfolioValue(string id, string currency)
        {
            if (!ObjectId.TryParse(id, out var objectId) || !Enum.TryParse<Currencies>(currency, out var currencyValue))
            {
                return BadRequest();
            }

            PortfolioModel portfolio = await _service.GetAsync(objectId);
            if (portfolio == null)
            {
                return NotFound();
            }

            var stockPrices = new List<StockPrice>();
            foreach (var stock in portfolio.Stocks)
            {
                var stockPrice = await _stocksService.GetStockPriceAsync(stock.Ticker);
                stockPrices.Add(stockPrice);    
            }

            var totalAmount = 0M;
            foreach (var stockPrice in stockPrices)
            {
                var currencyRate = await _currencyRateService.GetCurrencyRateAsync(currencyValue, stockPrice.Currency);
                var valueInCurrency = stockPrice.Price * currencyRate.Rate;
                totalAmount += valueInCurrency;
            }
            
            return Ok(totalAmount);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio(string id)
        {
            if (!ObjectId.TryParse(id, out var value))
            {
                return BadRequest();
            }

            await _service.SoftDeleteAsync(value);
            return Ok();
        }
    }
}
