using System.ComponentModel;
using System.Runtime.Serialization;
using Binance.API.Csharp.Client.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance.API.Csharp.Client.Models.Market.TradingRules
{
    public class Filter
    {
        [JsonProperty("filterType")]
        public ExchangeFilterType FilterType { get; set; }

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

    [JsonConverter(typeof(DefaultValueEnumConverter))]
    [DefaultValue(Unknown)]
    public enum ExchangeFilterType
    {
        Unknown = -1000,

        [EnumMember(Value = "PRICE_FILTER")]
        PriceFilter,

        [EnumMember(Value = "LOT_SIZE")]
        LotSize,

        [EnumMember(Value = "MIN_NOTIONAL")]
        MinNotional,

        [EnumMember(Value = "MAX_NUM_ALGO_ORDERS")]
        MaxNumAlgoOrders,

        [EnumMember(Value = "ICEBERG_PARTS")]
        IcebergParts,

        [EnumMember(Value = "EXCHANGE_MAX_NUM_ORDERS")]
        ExchangeMaxNumOrders,

        [EnumMember(Value = "EXCHANGE_MAX_NUM_ALGO_ORDERS")]
        ExchangeMaxNumAlgoOrders
    }
}