using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace MarketData.Models
{
    public static class YahooHelper
    {
        public static async Task<List<YahooStockData>> GetYahooStock(string ticker, string start, string end, string period)
        {
            var p = Period.Daily;
            if (period.ToLower() == "weekly") p = Period.Weekly;
            else if (period.ToLower() == "monthly") p = Period.Monthly;
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            var hist = await Yahoo.GetHistoricalAsync(ticker, startDate, endDate, p);

            List<YahooStockData> prices = new List<YahooStockData>();
            foreach (var r in hist)
            {
                prices.Add(new YahooStockData
                {
                    Ticker = ticker,
                    Date = r.DateTime,
                    Open = (double)r.Open,
                    High = (double)r.High,
                    Low = (double)r.Low,
                    Close = (double)r.Close,
                    CloseAdj = (double)r.AdjustedClose,
                    Volume = r.Volume
                });
            }
            return prices;
        }

        public static OptionResult GetYahooOption(string ticker, DateTime? expiry = null)
        {
            string url = "https://query1.finance.yahoo.com/v7/finance/options/" + ticker;
            if (expiry != null)
            {
                var expiry1 = (DateTime)expiry;
                var date = new DateTime(expiry1.Year, expiry1.Month, expiry1.Day, 0, 0, 0, DateTimeKind.Utc);
                var unixDate = ((DateTimeOffset)date).ToUnixTimeSeconds();
                url += "?date=" + unixDate;
            }          
           
            string opt = string.Empty;

            using (WebClient wc = new WebClient())
            {
                try
                {
                    opt = wc.DownloadString(url);
                }
                catch { }
            }

            dynamic json = JsonConvert.DeserializeObject(opt);

            var res = new OptionResult();
            res.Expirations = new List<string>();
            foreach (var d in json.optionChain.result[0].expirationDates)
            {
                res.Expirations.Add(FromUnixTime(d.ToString()));
            }
            res.StockData = new YahooStockData();
            res.StockData.Date = DateTime.Parse(FromUnixTime(json.optionChain.result[0].quote.regularMarketTime.ToString()));
            res.StockData.Open = double.Parse(json.optionChain.result[0].quote.regularMarketOpen.ToString());
            res.StockData.High = double.Parse(json.optionChain.result[0].quote.regularMarketDayHigh.ToString());
            res.StockData.Low = double.Parse(json.optionChain.result[0].quote.regularMarketDayLow.ToString());
            res.StockData.Close = double.Parse(json.optionChain.result[0].quote.regularMarketPrice.ToString());
            res.StockData.Volume = double.Parse(json.optionChain.result[0].quote.regularMarketVolume.ToString());

            res.CallOptions = new List<OptionData>();
            foreach (var d in json.optionChain.result[0].options[0].calls)
            {
                res.CallOptions.Add(new OptionData
                {
                    ContractSymbol = d.contractSymbol.ToString(),
                    Strike = decimal.Parse(d.strike.ToString()),
                    Currency = d.currency.ToString(),
                    LastPrice = decimal.Parse(d.lastPrice.ToString()),
                    Change = decimal.Parse(d.change.ToString()),
                    Volume = int.Parse(d.volume.ToString()),
                    OpenInterest = int.Parse(d.openInterest.ToString()),
                    Bid = decimal.Parse(d.bid.ToString()),
                    Ask = decimal.Parse(d.ask.ToString()),
                    ContractSize = d.contractSize.ToString(),
                    Expiration = FromUnixTime(d.expiration.ToString()),
                    LastTradeDate = FromUnixTime(d.lastTradeDate.ToString()),
                    ImpliedVol = double.Parse(d.impliedVolatility.ToString()),
                    InTheMoneny = bool.Parse(d.inTheMoney.ToString())
                });
            }
            res.PutOptions = new List<OptionData>();
            foreach (var d in json.optionChain.result[0].options[0].puts)
            {
                res.PutOptions.Add(new OptionData
                {
                    ContractSymbol = d.contractSymbol.ToString(),
                    Strike = decimal.Parse(d.strike.ToString()),
                    Currency = d.currency.ToString(),
                    LastPrice = decimal.Parse(d.lastPrice.ToString()),
                    Volume = int.Parse(d.volume.ToString()),
                    Bid = decimal.Parse(d.bid.ToString()),
                    Ask = decimal.Parse(d.ask.ToString()),
                    ContractSize = d.contractSize.ToString(),
                    Expiration = FromUnixTime(d.expiration.ToString()),
                    LastTradeDate = FromUnixTime(d.lastTradeDate.ToString()),
                    ImpliedVol = double.Parse(d.impliedVolatility.ToString()),
                    InTheMoneny = bool.Parse(d.inTheMoney.ToString())
                });
            }
            return res;
        }


        private static string FromUnixTime(string utime)
        {
            return (DateTimeOffset.FromUnixTimeSeconds(long.Parse(utime)).DateTime.ToUniversalTime()).ToShortDateString();
        }
    }
}
