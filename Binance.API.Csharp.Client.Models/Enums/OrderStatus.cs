using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Binance.API.Csharp.Client.Models.Enums
{
    public enum OrderStatus
    {
        New,
        [EnumMember(Value = "PARTIALLY_FILLED")]
        PartiallyFilled,
        Filled,
        Canceled,
        [EnumMember(Value = "PENDING_CANCEL")]
        PendingCancel,
        Rejected,
        Expired,
    }
}