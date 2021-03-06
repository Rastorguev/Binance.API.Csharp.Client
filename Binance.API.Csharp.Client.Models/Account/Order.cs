﻿using System;
using Binance.API.Csharp.Client.Models.Enums;
using Binance.API.Csharp.Client.Models.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance.API.Csharp.Client.Models.Account
{
    public class Order : IOrder
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("side")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }
        [JsonProperty("origQty")]
        public decimal OrderQuantity { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }
        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; }
        [JsonProperty("executedQty")]
        public decimal ExecutedQty { get; set; }
        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }
        [JsonProperty("stopPrice")]
        public decimal StopPrice { get; set; }
        [JsonProperty("icebergQty")]
        public decimal IcebergQty { get; set; }
        [JsonProperty("time")]
        public long UnixTime { get; set; }
    }
}