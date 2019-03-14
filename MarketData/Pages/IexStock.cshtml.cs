using IEXTradingDotNetCore.STOCK_CHART;
using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace MarketData.Pages
{
    public class IexStockModel : PageModel
    {
        [BindProperty]
        public IexStocks IexModel { get; set; } = new IexStocks();

        public void OnGet()
        {
            IexModel.Ticker = "IBM";
            IexModel.Range = Const_STOCK_CHART.STOCK_CHART_input_options_fields.OneMonth;
            IexModel.StockData = IexHelper.GetIexStock(IexModel.Ticker, IexModel.Range);
        }

        public void OnPost()
        {
            IexModel.StockData = IexHelper.GetIexStock(IexModel.Ticker, IexModel.Range);
        }        
    }

    public class IexStocks
    {
        public string Ticker { get; set; }
        public Const_STOCK_CHART.STOCK_CHART_input_options_fields Range { get; set; }
        public List<YahooStockData> StockData { get; set; }
    }
}