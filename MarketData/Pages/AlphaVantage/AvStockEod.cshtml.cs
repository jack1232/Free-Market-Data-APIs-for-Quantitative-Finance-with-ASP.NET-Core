using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace MarketData.Pages.AlphaVantage
{
    public class AvStockEodModel : PageModel
    {
        private readonly ApiKeySettings.AlphaVantage avKey;
        [BindProperty]
        public AvEodStocks AvModel { get; set; } = new AvEodStocks();

        public AvStockEodModel(IOptions<ApiKeySettings.AlphaVantage> alpha)
        {
            avKey = alpha.Value;
        }

        public void OnGet()
        {
            AvModel.Ticker = "IBM";
            AvModel.StartDate = DateTime.Parse("2018-01-01");
            AvModel.EndDate = DateTime.Parse("2018-03-01");
            AvModel.StockData = AlphaVantageHelper.GetAvStockEod(AvModel.Ticker, AvModel.StartDate, AvModel.EndDate, AvModel.Period, avKey.ApiKey);
        }

        public void OnPost()
        {
            AvModel.StockData = AlphaVantageHelper.GetAvStockEod(AvModel.Ticker, AvModel.StartDate, AvModel.EndDate, AvModel.Period, avKey.ApiKey);
        }
    }

    public class AvEodStocks
    {
        public string Ticker { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Period { get; set; }
        public List<YahooStockData> StockData { get; set; }
    }
}