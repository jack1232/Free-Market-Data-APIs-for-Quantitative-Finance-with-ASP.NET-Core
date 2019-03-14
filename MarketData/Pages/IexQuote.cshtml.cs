using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MarketData.Pages
{
    public class IexQuoteModel : PageModel
    {
        [BindProperty]
        public IexQuotes QuoteModel { get; set; } = new IexQuotes();

        public void OnGet()
        {
            QuoteModel.Ticker = "IBM";
            QuoteModel.StockQuote = IexHelper.GetIexQuote(QuoteModel.Ticker);
        }

        public void OnPost()
        {
            QuoteModel.StockQuote = IexHelper.GetIexQuote(QuoteModel.Ticker);
        }
    }

    public class IexQuotes
    {
        public string Ticker { get; set; }
        public IexStockQuote StockQuote { get; set; }
    }
}