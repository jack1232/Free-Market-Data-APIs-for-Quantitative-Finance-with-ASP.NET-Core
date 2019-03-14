using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketData.Pages
{
    public class TiingoStockModel : PageModel
    {
        private readonly ApiKeySettings.Tiingo tiingoToken;
        [BindProperty]
        public TiingolStocks TiingolModel { get; set; } = new TiingolStocks();

        public TiingoStockModel(IOptions<ApiKeySettings.Tiingo> tiingo)
        {
            tiingoToken = tiingo.Value;           
        }

        public async Task OnGetAsync()
        {
            //set default values
            TiingolModel.Ticker = "MSFT";
            TiingolModel.StartDate = DateTime.Parse("2019-01-01");
            TiingolModel.EndDate = DateTime.Parse("2019-03-01");
            var start = TiingolModel.StartDate.ToString("yyyy-MM-dd");
            var end = TiingolModel.EndDate.ToString("yyyy-MM-dd");
            TiingolModel.StockData = await TiingoHelper.GetTiingoStock(TiingolModel.Ticker, start, end, tiingoToken.ApiKey);
        }

        public async Task OnPost()
        {
            var start = TiingolModel.StartDate.ToString("yyyy-MM-dd");
            var end = TiingolModel.EndDate.ToString("yyyy-MM-dd");
            TiingolModel.StockData = await TiingoHelper.GetTiingoStock(TiingolModel.Ticker, start, end, tiingoToken.ApiKey);
        }
    }

    public class TiingolStocks
    {
        public string Ticker { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AdjStockData> StockData { get; set; }
    }
}