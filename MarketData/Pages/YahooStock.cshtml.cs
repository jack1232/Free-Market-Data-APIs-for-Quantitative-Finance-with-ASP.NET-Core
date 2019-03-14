using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketData.Pages
{
    public class YahooStockModel : PageModel
    {
        [BindProperty]
        public YahooStocks YahooModel { get; set; } = new YahooStocks();

        public async Task OnGet()
        {
            YahooModel.Ticker = "IBM";
            YahooModel.StartDate = DateTime.Parse("2018-01-01");
            YahooModel.EndDate = DateTime.Parse("2018-03-01");
            YahooModel.Period = "daily";
            var start = YahooModel.StartDate.ToString("yyyy-MM-dd");
            var end = YahooModel.EndDate.ToString("yyyy-MM-dd");
            YahooModel.StockData = await YahooHelper.GetYahooStock(YahooModel.Ticker, start, end, YahooModel.Period);
        }

        public async Task OnPost()
        {
            var start = YahooModel.StartDate.ToString("yyyy-MM-dd");
            var end = YahooModel.EndDate.ToString("yyyy-MM-dd");
            YahooModel.StockData = await YahooHelper.GetYahooStock(YahooModel.Ticker, start, end, YahooModel.Period);
        }
    }

    public class YahooStocks
    {
        public string Ticker { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Period { get; set; }
        public List<YahooStockData> StockData { get; set; }
    }
}