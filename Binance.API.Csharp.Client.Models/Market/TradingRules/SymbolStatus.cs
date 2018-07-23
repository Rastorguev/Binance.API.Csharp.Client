using System.Runtime.Serialization;

namespace Binance.API.Csharp.Client.Models.Market.TradingRules
{
    public enum SymbolStatus
    {
        [EnumMember(Value = "PRE_TRADING")]
        PreTrading,

        [EnumMember(Value = "TRADING")]
        Trading,

        [EnumMember(Value = "POST_TRADING,")]
        PostTrading,

        [EnumMember(Value = "END_OF_DAY")]
        EndOfDay,

        [EnumMember(Value = "HALT")]
        Halt,

        [EnumMember(Value = "AUCTION_MATCH")]
        AuctionMatch,

        [EnumMember(Value = "BREAK")]
        Break
    }
}