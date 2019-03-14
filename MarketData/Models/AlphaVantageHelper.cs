using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MarketData.Models
{
    public static class AlphaVantageHelper
    {
        public static List<YahooStockData> GetAvStockEod(string ticker, DateTime start, DateTime end, string period, string apiKey)
        {
            var size = "compact";
            if (start < DateTime.Today.AddDays(-120)) size = "full";
            var tseries = "TIME_SERIES_DAILY_ADJUSTED";
            if (period == "weekly") tseries = "TIME_SERIES_WEEKLY_ADJUSTED";
            else if (period == "monthly") tseries = "TIME_SERIES_MONTHLY_ADJUSTED";
            string url = "https://www.alphavantage.co/query?function=[tseries]&symbol=[ticker]&outputsize=[size]&apikey=[apiKey]&datatype=csv";
            url = url.Replace("[tseries]", tseries);
            url = url.Replace("[ticker]", ticker);
            url = url.Replace("[size]", size);
            url = url.Replace("[apiKey]", apiKey);

            string history = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    history = wc.DownloadString(url);
                }
                catch { }
            }

            history = history.Replace("\r", "");
            string[] rows = history.Split('\n');

            var models = new List<YahooStockData>();
            for (var i = 1; i < rows.Length; i++)
            {
                var r = rows[i].Split(",");
                try
                {
                    var date = DateTime.Parse(r[0]);
                    if (date >= start && date <= end)
                    {
                        models.Add(new YahooStockData
                        {
                            Ticker = ticker,
                            Date = date,
                            Open = double.Parse(r[1]),
                            High = double.Parse(r[2]),
                            Low = double.Parse(r[3]),
                            Close = double.Parse(r[4]),
                            CloseAdj = double.Parse(r[5]),
                            Volume = double.Parse(r[6])
                        });
                    }
                }
                catch { }
            }
            return models.OrderBy(d => d.Date).ToList();
        }

        public static List<YahooStockData> GetAvStockBar(string ticker, int interval, int outputsize, string apiKey)
        {
            var size = "compact";
            if (outputsize > 100) size = "full";
            var tseries = "TIME_SERIES_INTRADAY";
            string url = "https://www.alphavantage.co/query?function=[tseries]&symbol=[ticker]&outputsize=[size]&apikey=[apiKey]&datatype=csv&interval=[interval]min";
            url = url.Replace("[tseries]", tseries);
            url = url.Replace("[ticker]", ticker);
            url = url.Replace("[size]", size);
            url = url.Replace("[apiKey]", apiKey);
            url = url.Replace("[interval]", interval.ToString());

            string history = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    history = wc.DownloadString(url);
                }
                catch { }
            }
            history = history.Replace("\r", "");
            string[] rows = history.Split('\n');
            var models = new List<YahooStockData>();

            for (var i = 1; i < rows.Length; i++)
            {
                var r = rows[i].Split(",");
                try
                {
                    var date = DateTime.Parse(r[0]);
                    models.Add(new YahooStockData
                    {
                        Ticker = ticker,
                        Date = date,
                        Open = double.Parse(r[1]),
                        High = double.Parse(r[2]),
                        Low = double.Parse(r[3]),
                        Close = double.Parse(r[4]),
                        Volume = double.Parse(r[5])
                    });
                }
                catch { }
            }
            return models.OrderBy(d => d.Date).ToList();
        }

        public static AvStockQuote GetAvStockQuote(string ticker, string apiKey)
        {
            var function = "GLOBAL_QUOTE";
            string url = "https://www.alphavantage.co/query?function=[function]&symbol=[ticker]&apikey=[apiKey]&datatype=csv";
            url = url.Replace("[function]", function);
            url = url.Replace("[ticker]", ticker);
            url = url.Replace("[apiKey]", apiKey);
            string quote = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    quote = wc.DownloadString(url);
                }
                catch { }
            }
            quote = quote.Replace("\r", "");
            string[] rows = quote.Split('\n');
            AvStockQuote model = null; ;
            if (rows.Length > 1)
            {
                var r = rows[1].Split(",");
                model = new AvStockQuote
                {
                    Ticker = ticker,
                    TimeStamp = DateTime.Now,
                    Open = decimal.Parse(r[1]),
                    High = decimal.Parse(r[2]),
                    Low = decimal.Parse(r[3]),
                    Price = decimal.Parse(r[4]),
                    Volume = decimal.Parse(r[5]),
                    PrevClose = decimal.Parse(r[7]),
                    Change = decimal.Parse(r[8]),
                    ChangePercent = decimal.Parse(r[9].TrimEnd(new char[] { '%', ' ' })) / 100M
                };
            }
            return model;
        }

        public static List<AvFxEod> GetAvFxEod(string ticker, DateTime start, DateTime end, string period, string apiKey)
        {
            string history = string.Empty;
            if (ticker.Length != 6) return null;
            var fromTicker = ticker.Substring(0, 3);
            var toTicker = ticker.Substring(3, 3);
            var size = "compact";
            if (start < DateTime.Today.AddDays(-120)) size = "full";
            var function = "FX_DAILY";
            if (period == "weekly") function = "FX_WEEKLY";
            else if (period == "monthly") function = "FX_MONTHLY";
            string url = "https://www.alphavantage.co/query?function=[function]&from_symbol=[fromTicker]&to_symbol=[toTicker]&outputsize=[size]&apikey=[apiKey]&datatype=csv";
            url = url.Replace("[function]", function);
            url = url.Replace("[fromTicker]", fromTicker);
            url = url.Replace("[toTicker]", toTicker);
            url = url.Replace("[size]", size);
            url = url.Replace("[apiKey]", apiKey);
            using (WebClient wc = new WebClient())
            {
                try
                {
                    history = wc.DownloadString(url);
                }
                catch { }
            }
            history = history.Replace("\r", "");
            string[] rows = history.Split('\n');

            var models = new List<AvFxEod>();
            for (var i = 1; i < rows.Length; i++)
            {
                var r = rows[i].Split(",");
                try
                {
                    var date = DateTime.Parse(r[0]);
                    if (date >= start && date <= end)
                    {
                        models.Add(new AvFxEod
                        {
                            Ticker = ticker,
                            Date = date,
                            Open = decimal.Parse(r[1]),
                            High = decimal.Parse(r[2]),
                            Low = decimal.Parse(r[3]),
                            Close = decimal.Parse(r[4])
                        });
                    }
                }
                catch { }
            }
            return models.OrderBy(d => d.Date).ToList();
        }

        public static List<AvFxEod> GetAvFxBar(string ticker, int interval, int outputsize, string apiKey)
        {
            string history = string.Empty;
            if (ticker.Length != 6) return null;
            var fromTicker = ticker.Substring(0, 3);
            var toTicker = ticker.Substring(3, 3);
            var function = "FX_INTRADAY";
            var size = "compact";
            if (outputsize > 100) size = "full";

            string url = "https://www.alphavantage.co/query?function=[function]&from_symbol=[fromTicker]&to_symbol=[toTicker]&interval=[interval]min&outputsize=[size]&apikey=[apiKey]&datatype=csv";
            url = url.Replace("[function]", function);
            url = url.Replace("[fromTicker]", fromTicker);
            url = url.Replace("[toTicker]", toTicker);
            url = url.Replace("[size]", size);
            url = url.Replace("[interval]", interval.ToString());
            url = url.Replace("[apiKey]", apiKey);

            using (WebClient wc = new WebClient())
            {
                try
                {
                    history = wc.DownloadString(url);
                }
                catch { }
            }
            history = history.Replace("\r", "");
            string[] rows = history.Split('\n');

            var models = new List<AvFxEod>();
            for (var i = 1; i < rows.Length; i++)
            {
                var r = rows[i].Split(",");
                try
                {
                    var date = DateTime.Parse(r[0]);
                    models.Add(new AvFxEod
                    {
                        Ticker = ticker,
                        Date = date,
                        Open = decimal.Parse(r[1]),
                        High = decimal.Parse(r[2]),
                        Low = decimal.Parse(r[3]),
                        Close = decimal.Parse(r[4])
                    });
                }
                catch { }
            }
            return models.OrderBy(d => d.Date).ToList();
        }

        public static List<AvSectorPerf> GetAvSectorPerf(string apiKey)
        {
            string url = "https://www.alphavantage.co/query?function=SECTOR&apikey=[apiKey]";
            url = url.Replace("[apiKey]", apiKey);
            string result = string.Empty;

            using (WebClient wc = new WebClient())
            {
                try
                {
                    result = wc.DownloadString(url);
                }
                catch { }
            }
            List<AvSectorPerf> models = new List<AvSectorPerf>();
            var res = JsonConvert.DeserializeObject<dynamic>(result);
            string[] ranks = new string[]
            {
                "Rank A: Real-Time Performance",
                "Rank B: 1 Day Performance",
                "Rank C: 5 Day Performance",
                "Rank D: 1 Month Performance",
                "Rank E: 3 Month Performance",
                "Rank F: Year-to-Date (YTD) Performance",
                "Rank G: 1 Year Performance",
                "Rank H: 3 Year Performance",
                "Rank I: 5 Year Performance",
                "Rank J: 10 Year Performance"
            };

            foreach (var rank in ranks)
            {
                models.Add(new AvSectorPerf
                {
                    Rank = rank,
                    CommunicationServices = res[rank]["Communication Services"],
                    ConsumerDiscretionary = res[rank]["Consumer Discretionary"],
                    ConsumerStaples = res[rank]["Consumer Staples"],
                    Energy = res[rank]["Energy"],
                    Financials = res[rank]["Financials"],
                    HealthCare = res[rank]["Health Care"],
                    Industrials = res[rank]["Industrials"],
                    InformationTechnology = res[rank]["Information Technology"],
                    Matericals = res[rank]["Materials"],
                    Utilities = res[rank]["Utilities"]
                });
            }
            return models;
        }

    }
}
