using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace MarketData.Pages.AlphaVantage
{
    public class AvFxEodModel : PageModel
    {
        private readonly ApiKeySettings.AlphaVantage avKey;
        [BindProperty]
        public AvEodFxs AvModel { get; set; } = new AvEodFxs();

        public AvFxEodModel(IOptions<ApiKeySettings.AlphaVantage> alpha)
        {
            avKey = alpha.Value;
        }

        public void OnGet()
        {
            AvModel.Ticker = "USDEUR";
            AvModel.StartDate = DateTime.Parse("2018-01-01");
            AvModel.EndDate = DateTime.Parse("2018-03-01");
            AvModel.Period = "daily";
            AvModel.FxData = AlphaVantageHelper.GetAvFxEod(AvModel.Ticker, AvModel.StartDate, AvModel.EndDate, AvModel.Period, avKey.ApiKey);
        }

        public void OnPost()
        {
            AvModel.FxData = AlphaVantageHelper.GetAvFxEod(AvModel.Ticker, AvModel.StartDate, AvModel.EndDate, AvModel.Period, avKey.ApiKey);
        }

        public class AvEodFxs
        {
            public string Ticker { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Period { get; set; }
            public List<AvFxEod> FxData { get; set; }
        }
    }
}