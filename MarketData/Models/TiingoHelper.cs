using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MarketData.Models
{
    public static class TiingoHelper
    {
        public static async Task<List<AdjStockData>> GetTiingoStock(string ticker, string start, string end, string apiKey)
        {
            string url = "https://api.tiingo.com/tiingo/daily/[ticker]/prices?startDate=[startDate]&endDate=[endDate]";
            url = url.Replace("[ticker]", ticker);
            url = url.Replace("[startDate]", start);
            url = url.Replace("[endDate]", end);
            string history = string.Empty;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey);
            history = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<AdjStockData>>(history);
        }
    }
}
