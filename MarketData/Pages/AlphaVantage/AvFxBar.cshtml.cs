using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MarketData.Pages.AlphaVantage
{
    public class AvFxBarModel : PageModel
    {
        private readonly ApiKeySettings.AlphaVantage avKey;
        [BindProperty]
        public AvBarFxs AvModel { get; set; } = new AvBarFxs();

        public AvFxBarModel(IOptions<ApiKeySettings.AlphaVantage> alpha)
        {
            avKey = alpha.Value;
        }

        public void OnGet()
        {
            AvModel.Ticker = "USDJPY";
            AvModel.Interval = 1;
            AvModel.Outputsize = 1;
            AvModel.FxData = AlphaVantageHelper.GetAvFxBar(AvModel.Ticker, AvModel.Interval, AvModel.Outputsize, avKey.ApiKey);
        }

        public void OnPost()
        {
            AvModel.FxData = AlphaVantageHelper.GetAvFxBar(AvModel.Ticker, AvModel.Interval, AvModel.Outputsize, avKey.ApiKey);
        }
    }

    public class AvBarFxs
    {
        public string Ticker { get; set; }
        public int Interval { get; set; }
        public int Outputsize { get; set; }
        public List<AvFxEod> FxData { get; set; }

    }
}