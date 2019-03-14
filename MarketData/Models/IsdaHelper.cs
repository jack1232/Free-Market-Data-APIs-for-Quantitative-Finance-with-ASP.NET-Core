using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml.Linq;

namespace MarketData.Models
{
    public static class IsdaHelper
    {
        public static IsdaRate GetIsdaRates(string currency, string date_yyyyMMdd)
        {
            string url = "https://www.markit.com/news/InterestRates_[ccy]_[date].zip";
            url = url.Replace("[ccy]", currency.ToUpper());
            url = url.Replace("[date]", date_yyyyMMdd);
            IsdaRate isda = new IsdaRate();
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var data = wc.DownloadData(url);
                    var stream = new MemoryStream(data);
                    using (ZipArchive archive = new ZipArchive(stream))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName.EndsWith(".xml",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                XDocument doc = XDocument.Load(entry.Open());
                                isda = ProcessIadaRates(doc, currency);
                            }
                        }
                    }
                }
                catch { }
            }
            return isda;
        }

        private static IsdaRate ProcessIadaRates(XDocument doc, string currency)
        {
            IsdaRate isda = new IsdaRate();
            isda.DepositRates = new List<ParRate>();
            isda.SwapRates = new List<ParRate>();
            XElement asof = doc.Root.Element("effectiveasof");

            //Process deposits:
            XElement deposits = doc.Root.Element("deposits");
            string dayConvention = deposits.Element("daycountconvention").Value;
            string[] ss = deposits.Element("snaptime").Value.Split('T');
            string ts = ss[0] + " " + ss[1].Split('Z')[0];
            DateTime snapDate = Convert.ToDateTime(ts);
            string spotDate = deposits.Element("spotdate").Value;

            foreach (var e in deposits.Elements())
            {
                if (e.Name.ToString() == "curvepoint")
                {
                    isda.Currency = currency;
                    isda.EffectiveAsOf = asof.Value;
                    isda.SnapTime = snapDate;
                    isda.SpotDate = spotDate;
                    isda.DepositDayCountConvention = dayConvention;

                    isda.DepositRates.Add(new ParRate
                    {
                        Tenor = e.Element("tenor").Value,
                        Rate = e.Element("parrate").Value,
                        Maturity = e.Element("maturitydate").Value
                    });
                }
            }

            //Process swaps:
            XElement swaps = doc.Root.Element("swaps");
            dayConvention = swaps.Element("floatingdaycountconvention").Value;
            string fixDayConvention = swaps.Element("fixeddaycountconvention").Value;
            ss = swaps.Element("snaptime").Value.Split('T');
            ts = ss[0] + " " + ss[1].Split('Z')[0];
            snapDate = Convert.ToDateTime(ts);
            spotDate = swaps.Element("spotdate").Value;
            string floatPay = swaps.Element("floatingpaymentfrequency").Value;
            string fixPay = swaps.Element("fixedpaymentfrequency").Value;
            foreach (var e in swaps.Elements())
            {
                if (e.Name.ToString() == "curvepoint")
                {
                    isda.SwapFixedDayCountConvention = fixDayConvention;
                    isda.SwapFloatingDayCountConvention = dayConvention;
                    isda.SwapFixedPaymentFrequency = fixPay;
                    isda.SwapFloatingPaymentFrequency = floatPay;

                    isda.SwapRates.Add(new ParRate
                    {
                        Tenor = e.Element("tenor").Value,
                        Rate = e.Element("parrate").Value,
                        Maturity = e.Element("maturitydate").Value
                    });
                }
            }
            return isda;
        }
    }
}
