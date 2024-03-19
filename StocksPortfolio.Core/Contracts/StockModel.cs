namespace StocksPortfolio.Core.Contracts
{
    public class StockModel
    {
        public string Ticker { get; set; }
        public string Currency { get; set; }
        public int NumberOfShares { get; set; }
    }
}
