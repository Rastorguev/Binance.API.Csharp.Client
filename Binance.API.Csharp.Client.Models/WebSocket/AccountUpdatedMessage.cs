﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.API.Csharp.Client.Models.WebSocket
{
    public class AccountUpdatedMessage : WebSocketMessage
    {
        [JsonProperty("m")]
        public int MakerCommission { get; set; }
        [JsonProperty("t")]
        public int TakerCommission { get; set; }
        [JsonProperty("b")]
        public int BuyerCommission { get; set; }
        [JsonProperty("s")]
        public int SellerCommission { get; set; }
        [JsonProperty("T")]
        public bool CanTrade { get; set; }
        [JsonProperty("W")]
        public bool CanWithdraw { get; set; }
        [JsonProperty("D")]
        public bool CanDeposit { get; set; }
        [JsonProperty("B")]
        public IEnumerable<Balance> Balances { get; set; }
    }
    public class Balance
    {
        [JsonProperty("a")]
        public string Asset { get; set; }
        [JsonProperty("f")]
        public decimal Free { get; set; }
        [JsonProperty("l")]
        public decimal Locked { get; set; }
    }
}
