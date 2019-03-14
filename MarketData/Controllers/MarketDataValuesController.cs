using IEXTradingDotNetCore.STOCK_CHART;
using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketData.Controllers
{
    [Produces("application/json")]
    public class MarketDataValuesController : Controller
    {
        private readonly ApiKeySettings.AlphaVantage avKey;
        private readonly ApiKeySettings.Tiingo tngKey;

        public MarketDataValuesController(IOptions<ApiKeySettings.AlphaVantage> alpha, IOptions<ApiKeySettings.Tiingo> tng)
        {
            avKey = alpha.Value;
            tngKey = tng.Value;
        }

        [Route("~/api/YahooStock/{ticker}/{start}/{end}/{period}")]
        [HttpGet]
        public async Task<List<YahooStockData>> YahooStock(string ticker, string start, string end, string period)
        {
            return await YahooHelper.GetYahooStock(ticker, start, end, period);
        }

        [Route("~/api/YahooOption/{ticker}/{expiry}")]
        [HttpGet]
        public OptionResult YahooOption(string ticker, DateTime? expiry)
        {
            return YahooHelper.GetYahooOption(ticker, expiry);
        }

        [Route("~/api/IexStock/{ticker}/{range}")]
        [HttpGet]
        public List<YahooStockData> GetIexStock(string ticker,
            Const_STOCK_CHART.STOCK_CHART_input_options_fields range)
        {
            return IexHelper.GetIexStock(ticker, range);
        }

        [Route("~/api/IexQuote/{ticker}")]
        [HttpGet]
        public IexStockQuote GetIexQuote(string ticker)
        {
            return IexHelper.GetIexQuote(ticker);
        }

        [Route("~/api/AvEod/{ticker}/{start}/{end}/{period}")]
        [HttpGet]
        public List<YahooStockData> GetAvStockEod(string ticker, string start, string end, string period)
        {
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            return AlphaVantageHelper.GetAvStockEod(ticker, startDate, endDate, period, avKey.ApiKey);
        }

        [Route("~/api/TiingoEod/{ticker}/{start}/{end}")]
        [HttpGet]
        public async Task<List<AdjStockData>> GetTiingoStockEod(string ticker, string start, string end)
        {
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            return await TiingoHelper.GetTiingoStock(ticker, start, end, tngKey.ApiKey);
        }

        [Route("~/api/Isda/{ccy}/{date}")]
        [HttpGet]
        public IsdaRate GetIsdaRate(string ccy, string date)
        {
            return IsdaHelper.GetIsdaRates(ccy, date);
        }
    }
}
