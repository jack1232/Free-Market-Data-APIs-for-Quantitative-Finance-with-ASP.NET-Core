using IEXTrading;
using IEXTradingDotNetCore.STOCK_CHART;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketData.Models
{
    public static class IexHelper
    {
        public static List<YahooStockData> GetIexStock(string ticker,
            Const_STOCK_CHART.STOCK_CHART_input_options_fields range
            = Const_STOCK_CHART.STOCK_CHART_input_options_fields.OneMonth)
        {
            var connection = IEXTradingConnection.Instance;
            var operation = connection.GetQueryObject_STOCK_CHART();
            var response = operation.Query(ticker, range);
            var data = response.Data;

            var models = new List<YahooStockData>();

            foreach (var d in data.TimeSeries)
            {
                models.Add(new YahooStockData
                {
                    Ticker = ticker,
                    Date = DateTime.Parse(d.Date),
                    Open = double.Parse(d.Open),
                    High = double.Parse(d.High),
                    Low = double.Parse(d.Low),
                    Close = double.Parse(d.Close),
                    Volume = double.Parse(d.Volume)

                });
            }
            return models.OrderBy(d => d.Date).ToList();
        }

        public static IexStockQuote GetIexQuote(string ticker)
        {
            var connection = IEXTradingConnection.Instance;
            var operation = connection.GetQueryObject_STOCK_QUOTE();
            var response = operation.Query(ticker);
            var data = response.Data;

            IexStockQuote result = null;           
            try
            {
                result = new IexStockQuote
                {
                    Ticker = ticker,
                    Open = decimal.Parse(data.Open),
                    OpenTime = FromUnixTime(data.OpenTime),
                    Close = decimal.Parse(data.Close),
                    CloseTime = FromUnixTime(data.CloseTime),
                    LatestPrice = decimal.Parse(data.LatestPrice),
                    LatestTime = DateTime.Parse(data.LatestTime),
                    LatestUpdateTime = FromUnixTime(data.LatestUpdate),
                    LatestVolume = double.Parse(data.LatestVolume),
                    DelayedPrice = decimal.Parse(data.DelayedPrice),
                    DelayedPriceTime = FromUnixTime(data.DelayedPriceTime),
                    PreviousClose = decimal.Parse(data.PreviousClose),
                    IexRealTimePrice = decimal.Parse(data.IEXRealtimePrice),
                    IexRealTimeSize = double.Parse(data.IEXRealtimeSize),
                    IexLastUpdated = FromUnixTime(data.IEXLastUpdated),
                    IexBidPrice = decimal.Parse(data.IEXBidPrice),
                    IexBidSize = decimal.Parse(data.IEXBidSize),
                    IexAskPrice = decimal.Parse(data.IEXAskPrice),
                    IexAskSize = decimal.Parse(data.IEXAskSize),
                    Change = double.Parse(data.Change),
                    ChangePercent = double.Parse(data.ChangePercent),
                    MarketCap = double.Parse(data.MarketCap),
                    PeRatio = double.Parse(data.PeRatio),
                    Week52High = decimal.Parse(data.Week52High),
                    Week52Low = decimal.Parse(data.Week52Low),
                    YtdChange = double.Parse(data.YtdChange)
                };
            }
            catch{ }
            return result;
        }

        private static DateTime FromUnixTime(string utime)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(utime))
                                 .DateTime.ToLocalTime();
        }
    }
}
