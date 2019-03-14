using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MarketData.Pages.AlphaVantage
{
    public class AvSectorModel : PageModel
    {
        private readonly ApiKeySettings.AlphaVantage avKey;
        [BindProperty]
        public List<AvSectorPerf> AvModel { get; set; } = new List<AvSectorPerf>();

        public AvSectorModel(IOptions<ApiKeySettings.AlphaVantage> alpha)
        {
            avKey = alpha.Value;
            AvModel = AlphaVantageHelper.GetAvSectorPerf(avKey.ApiKey);
        }

        public void OnPost()
        {
            AvModel = AlphaVantageHelper.GetAvSectorPerf(avKey.ApiKey);
        }
    }
}