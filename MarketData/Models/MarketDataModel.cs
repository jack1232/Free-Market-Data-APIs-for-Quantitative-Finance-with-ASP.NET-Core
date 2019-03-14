using System;
using System.Collections.Generic;

namespace MarketData.Models
{
    public class YahooStockData
    {
        public string Ticker { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double CloseAdj { get; set; }
        public double Volume { get; set; }
    }

    public class OptionResult
    {
        public List<string> Expirations { get; set; }
        public YahooStockData StockData { get; set; }
        public List<OptionData> CallOptions { get; set; }
        public List<OptionData> PutOptions { get; set; }
    }   

    public class OptionData
    {
        public string ContractSymbol { get; set; }
        public decimal Strike { get; set; }
        public string Currency { get; set; }
        public decimal LastPrice { get; set; }
        public decimal Change { get; set; }
        public int Volume { get; set; }
        public int OpenInterest { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public string ContractSize { get; set; }
        public string Expiration { get; set; }
        public string LastTradeDate { get; set; }
        public double ImpliedVol { get; set; }
        public Boolean InTheMoneny { get; set; }
    }

    public class IexStockQuote
    {
        public string Ticker { get; set; }
        public decimal Open { get; set; }
        public DateTime OpenTime { get; set; }
        public decimal Close { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal LatestPrice { get; set; }
        public DateTime LatestTime { get; set; }
        public DateTime LatestUpdateTime { get; set; }
        public double LatestVolume { get; set; }
        public decimal DelayedPrice { get; set; }
        public DateTime DelayedPriceTime { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal IexRealTimePrice { get; set; }
        public double IexRealTimeSize { get; set; }
        public DateTime IexLastUpdated { get; set; }
        public decimal IexBidPrice { get; set; }
        public decimal IexBidSize { get; set; }
        public decimal IexAskPrice { get; set; }
        public decimal IexAskSize { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public double MarketCap { get; set; }
        public double PeRatio { get; set; }
        public decimal Week52High { get; set; }
        public decimal Week52Low { get; set; }
        public double YtdChange { get; set; }
    }

    public class AvStockQuote
    {
        public string Ticker { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal PrevClose { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
    }

    public class AvFxEod
    {
        public string Ticker { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
    }

    public class AvSectorPerf
    {
        public string Rank { get; set; }
        public string CommunicationServices { get; set; }
        public string ConsumerDiscretionary { get; set; }
        public string ConsumerStaples { get; set; }
        public string Energy { get; set; }
        public string Financials { get; set; }
        public string HealthCare { get; set; }
        public string Industrials { get; set; }
        public string InformationTechnology { get; set; }
        public string Matericals { get; set; }
        public string Utilities { get; set; }
    }

    public class AdjStockData
    {
        public string Ticker { get; set; }
        public DateTime Date { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Volume { get; set; }
        public double AdjClose { get; set; }
        public double AdjHigh { get; set; }
        public double AdjLow { get; set; }
        public double AdjOpen { get; set; }
        public double AdjVolume { get; set; }
        public double DivCash { get; set; }
        public double SplitFactor { get; set; }
    }

    public class IsdaRate
    {
        public string Currency { get; set; }
        public string EffectiveAsOf { get; set; }
        public DateTime SnapTime { get; set; }
        public string SpotDate { get; set; }

        public string DepositDayCountConvention { get; set; }

        public string SwapFixedDayCountConvention { get; set; }
        public string SwapFloatingDayCountConvention { get; set; }
        public string SwapFixedPaymentFrequency { get; set; }
        public string SwapFloatingPaymentFrequency { get; set; }

        public List<ParRate> DepositRates { get; set; }
        public List<ParRate> SwapRates { get; set; }
    }

    public class ParRate
    {
        public string Tenor { get; set; }
        public string Rate { get; set; }
        public string Maturity { get; set; }
    }
}
