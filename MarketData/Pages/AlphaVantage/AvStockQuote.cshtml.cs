using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MarketData.Pages.AlphaVantage
{
    public class AvStockQuoteModel : PageModel
    {
        private readonly ApiKeySettings.AlphaVantage avKey;
        [BindProperty]
        public AvStockQuote AvModel { get; set; } = new AvStockQuote();

        public AvStockQuoteModel(IOptions<ApiKeySettings.AlphaVantage> alpha)
        {
            avKey = alpha.Value;
        }

        public void OnGet()
        {
            AvModel.Ticker = "IBM";
            AvModel = AlphaVantageHelper.GetAvStockQuote(AvModel.Ticker, avKey.ApiKey);
        }

        public void OnPost()
        {
            AvModel = AlphaVantageHelper.GetAvStockQuote(AvModel.Ticker, avKey.ApiKey);
        }
    }
}