using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MarketData.Pages.AlphaVantage
{
    public class AvStockBarModel : PageModel
    {
        private readonly ApiKeySettings.AlphaVantage avKey;
        [BindProperty]
        public AvBarStocks AvModel { get; set; } = new AvBarStocks();

        public AvStockBarModel(IOptions<ApiKeySettings.AlphaVantage> alpha)
        {
            avKey = alpha.Value;
        }

        public void OnGet()
        {
            AvModel.Ticker = "IBM";
            AvModel.Interval = 1;
            AvModel.Outputsize = 1;
            AvModel.StockData = AlphaVantageHelper.GetAvStockBar(AvModel.Ticker, AvModel.Interval, AvModel.Outputsize, avKey.ApiKey);
        }

        public void OnPost()
        {
            AvModel.StockData = AlphaVantageHelper.GetAvStockBar(AvModel.Ticker, AvModel.Interval, AvModel.Outputsize, avKey.ApiKey);
        }
    }

    public class AvBarStocks
    {
        public string Ticker { get; set; }
        public int Interval { get; set; }
        public int Outputsize { get; set; }
        public List<YahooStockData> StockData { get; set; }
    }
}