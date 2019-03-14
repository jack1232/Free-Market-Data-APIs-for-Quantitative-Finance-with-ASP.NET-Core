using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketData.Pages
{
    public class QuandlStockModel : PageModel
    {
        private readonly ApiKeySettings.Quandl quandlKey;
        [BindProperty]
        public QuandlStocks QuandlModel { get; set; } = new QuandlStocks();

        public QuandlStockModel(IOptions<ApiKeySettings.Quandl> quandl)
        {
            quandlKey = quandl.Value;
        }

        public async Task OngetAsync()
        {
            QuandlModel.Ticker = "IBM";
            QuandlModel.StartDate = DateTime.Parse("2000-01-01");
            QuandlModel.EndDate = DateTime.Parse("2000-03-01");
            QuandlModel.StockData = await QuandlHelper.GetQuandlStock(QuandlModel.Ticker,
                QuandlModel.StartDate, QuandlModel.EndDate, quandlKey.ApiKey);
        }

        public async Task OnPostAsync()
        {
            QuandlModel.StockData = await QuandlHelper.GetQuandlStock(QuandlModel.Ticker, 
                QuandlModel.StartDate, QuandlModel.EndDate, quandlKey.ApiKey);
        }
    }

    public class QuandlStocks
    {
        public string Ticker { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AdjStockData> StockData { get; set; }
    }
}