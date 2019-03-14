using MarketData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MarketData.Pages
{
    public class IsdaRateModel : PageModel
    {
        [BindProperty]
        public Isda IsdaModel { get; set; } = new Isda();

        public void OnGet()
        {
            IsdaModel.Currency = "USD";
            IsdaModel.Date = "20190311";
            IsdaModel.IsdaRate = IsdaHelper.GetIsdaRates(IsdaModel.Currency, IsdaModel.Date);
        }

        public void OnPost()
        {
            IsdaModel.IsdaRate = IsdaHelper.GetIsdaRates(IsdaModel.Currency, IsdaModel.Date);
        }
    }

    public class Isda
    {
        public string Currency { get; set; }
        public string Date { get; set; }
        public IsdaRate IsdaRate { get; set; }
    }
}