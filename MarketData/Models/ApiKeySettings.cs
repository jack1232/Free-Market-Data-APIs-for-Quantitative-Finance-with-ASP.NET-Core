namespace MarketData.Models
{
    public class ApiKeySettings
    {
        public class AlphaVantage
        {
            public string ApiKey { get; set; }
        }

        public class Quandl
        {
            public string ApiKey { get; set; }
        }

        public class Tiingo
        {
            public string ApiKey { get; set; }
        }
    }
}
