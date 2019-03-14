using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quandl.NET;

namespace MarketData.Models
{
    public static class QuandlHelper
    {
        public static async Task<List<AdjStockData>> GetQuandlStock(string ticker, DateTime start, DateTime end, string apiKey)
        {
            var client = new QuandlClient(apiKey);
            var data = await client.Timeseries.GetDataAsync("WIKI", ticker, startDate: start, endDate: end);

            var models = new List<AdjStockData>();
            foreach (var d in data.DatasetData.Data)
            {
                models.Add(new AdjStockData
                {
                    Ticker = ticker,
                    Date = DateTime.Parse(d[0].ToString()),
                    Open = double.Parse(d[1].ToString()),
                    High = double.Parse(d[2].ToString()),
                    Low = double.Parse(d[3].ToString()),
                    Close = double.Parse(d[4].ToString()),
                    Volume = double.Parse(d[5].ToString()),
                    DivCash = double.Parse(d[6].ToString()),
                    SplitFactor = double.Parse(d[7].ToString()),
                    AdjOpen = double.Parse(d[8].ToString()),
                    AdjHigh = double.Parse(d[9].ToString()),
                    AdjLow = double.Parse(d[10].ToString()),
                    AdjClose = double.Parse(d[11].ToString()),
                    AdjVolume = double.Parse(d[12].ToString())
                });
            }
            return models.OrderBy(d => d.Date).ToList();
        }
    }
}
