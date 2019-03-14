using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace MarketData.Pages
{
    public class YahooOptionModel : PageModel
    {
        [BindProperty]
        public YahooOptions YahooModel { get; set; } = new YahooOptions();
        public void OnGet()
        {
            YahooModel.Ticker = "IBM";
            YahooModel.OptionType = "Call";
            YahooModel.OptionResult = YahooHelper.GetYahooOption(YahooModel.Ticker);
            YahooModel.Expiration = YahooModel.OptionResult.Expirations[0];
            YahooModel.Expirations = new SelectList(YahooModel.OptionResult.Expirations);
        }
        public void OnPost()
        {
            YahooModel.OptionResult = YahooHelper.GetYahooOption(YahooModel.Ticker, DateTime.Parse(YahooModel.Expiration));
            YahooModel.Expirations = new SelectList(YahooModel.OptionResult.Expirations);
            ViewData["data"] = "expiry = " + YahooModel.Expiration;
        }
    }

    public class YahooOptions
    {
        public string Ticker { get; set; }
        public string OptionType { get; set; }
        public string Expiration { get; set; }
        public SelectList Expirations { get; set; }        
        public OptionResult OptionResult { get; set; }
    }
}