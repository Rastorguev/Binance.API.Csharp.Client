using Binance.API.Csharp.Client.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance.API.Csharp.Client.Models.WebSocket
{
    public class OrderOrTradeUpdatedMessage : WebSocketMessage
    {
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("c")]
        public string NewClientOrderId { get; set; }
        [JsonProperty("S")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }
        [JsonProperty("o")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }
        [JsonProperty("f")]
        public string TimeInForce { get; set; }
        [JsonProperty("q")]
        public decimal OriginalQuantity { get; set; }
        [JsonProperty("p")]
        public decimal Price { get; set; }
        [JsonProperty("P")]
        public decimal StopPrice { get; set; }
        [JsonProperty("x")]
        public string ExecutionType { get; set; }
        [JsonProperty("X")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }
        [JsonProperty("r")]
        public string RejectReason { get; set; }
        [JsonProperty("i")]
        public int OrderId { get; set; }
        [JsonProperty("l")]
        public decimal LastFilledTradeQuantity { get; set; }
        [JsonProperty("z")]
        public decimal FilledTradesAccumulatedQuantity { get; set; }
        [JsonProperty("L")]
        public decimal LastFilledTradePrice { get; set; }
        [JsonProperty("n")]
        public decimal Commission { get; set; }
        [JsonProperty("N")]
        public string CommissionAsset { get; set; }
        [JsonProperty("T")]
        public long TradeTime { get; set; }
        [JsonProperty("t")]
        public int TradeId { get; set; }
        [JsonProperty("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
