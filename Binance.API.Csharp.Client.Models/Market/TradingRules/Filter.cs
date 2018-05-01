using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace Binance.API.Csharp.Client.Models.Market.TradingRules
{
    public class Filter
    {
        [JsonProperty("filterType")]
        public ExcangeFilterType FilterType { get; set; }
        [JsonProperty("minPrice")]
        public decimal MinPrice { get; set; }
        [JsonProperty("maxPrice")]
        public decimal MaxPrice { get; set; }
        [JsonProperty("tickSize")]
        public decimal TickSize { get; set; }
        [JsonProperty("minQty")]
        public decimal MinQty { get; set; }
        [JsonProperty("maxQty")]
        public decimal MaxQty { get; set; }
        [JsonProperty("stepSize")]
        public decimal StepSize { get; set; }
        [JsonProperty("minNotional")]
        public decimal MinNotional { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExcangeFilterType
    {
        [EnumMember(Value = "PRICE_FILTER")]
        PriceFilter,
        [EnumMember(Value = "LOT_SIZE")]
        LotSize,
        [EnumMember(Value = "MIN_NOTIONAL")]
        MinNotional
    }
}
