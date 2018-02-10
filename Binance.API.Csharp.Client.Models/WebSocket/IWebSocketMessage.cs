using Newtonsoft.Json;

namespace Binance.API.Csharp.Client.Models.WebSocket
{
    public interface IWebSocketMessage
    {
        string EventType { get; set; }
        long EventTime { get; set; }
    }

    public class WebSocketMessage : IWebSocketMessage
    {
        [JsonProperty("e")]
        public string EventType { get; set; }
        [JsonProperty("E")]
        public long EventTime { get; set; }
    }
}