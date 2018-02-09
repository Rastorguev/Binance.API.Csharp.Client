﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Binance.API.Csharp.Client.Domain.Abstract;
using Binance.API.Csharp.Client.Domain.Interfaces;
using Binance.API.Csharp.Client.Models.Enums;
using Binance.API.Csharp.Client.Models.WebSocket;
using Binance.API.Csharp.Client.Utils;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Binance.API.Csharp.Client
{
    public class ApiClient : ApiClientAbstract, IApiClient
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="apiKey">Key used to authenticate within the API.</param>
        /// <param name="apiSecret">API secret used to signed API calls.</param>
        /// <param name="apiUrl">API base url.</param>
        public ApiClient(string apiKey, string apiSecret, string apiUrl = @"https://www.binance.com",
            string webSocketEndpoint = @"wss://stream.binance.com:9443/ws/", bool addDefaultHeaders = true) : base(
            apiKey, apiSecret, apiUrl, webSocketEndpoint, addDefaultHeaders)
        {
        }

        /// <summary>
        /// Calls API Methods.
        /// </summary>
        /// <typeparam name="T">Type to which the response content will be converted.</typeparam>
        /// <param name="method">HTTPMethod (POST-GET-PUT-DELETE)</param>
        /// <param name="endpoint">Url endpoing.</param>
        /// <param name="isSigned">Specifies if the request needs a signature.</param>
        /// <param name="parameters">Request parameters.</param>
        /// <returns></returns>
        public async Task<T> CallAsync<T>(ApiMethod method, string endpoint, bool isSigned = false,
            string parameters = null)
        {
            var finalEndpoint = endpoint + (string.IsNullOrWhiteSpace(parameters) ? "" : $"?{parameters}");

            if (isSigned)
            {
                parameters += (!string.IsNullOrWhiteSpace(parameters) ? "&timestamp=" : "timestamp=") +
                              Utilities.GenerateTimeStamp(DateTime.Now.ToUniversalTime());

                var signature = Utilities.GenerateSignature(_apiSecret, parameters);
                finalEndpoint = $"{endpoint}?{parameters}&signature={signature}";
            }

            var request = new HttpRequestMessage(Utilities.CreateHttpMethod(method.ToString()), finalEndpoint);

            try
            {
                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }
                if (response.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    throw new BinanceApiException("Api Request Timeout.");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorPayload = JsonConvert.DeserializeObject<BinanceErrorPayload>(content);

                    throw new InvalidRequestException(errorPayload.ErrorCode, errorPayload.Message);
                }
            }

            catch (Exception ex) when (!(ex is BinanceApiException))
            {
                throw new BinanceApiException("Binance Api Error", ex);
            }

            throw new BinanceApiException("Binance Api Error");
        }

        /// <summary>
        /// Connects to a Websocket endpoint.
        /// </summary>
        /// <typeparam name="T">Type used to parsed the response message.</typeparam>
        /// <param name="parameters">Paremeters to send to the Websocket.</param>
        /// <param name="messageDelegate">Deletage to callback after receive a message.</param>
        /// <param name="useCustomParser">Specifies if needs to use a custom parser for the response message.</param>
        public void ConnectToWebSocket<T>(string parameters, MessageHandler<T> messageHandler,
            bool useCustomParser = false)
        {
            var finalEndpoint = _webSocketEndpoint + parameters;

            var ws = new WebSocket(finalEndpoint);

            ws.OnMessage += (sender, e) =>
            {
                dynamic eventData;

                if (useCustomParser)
                {
                    var customParser = new CustomParser();
                    eventData = customParser.GetParsedDepthMessage(JsonConvert.DeserializeObject<dynamic>(e.Data));
                }
                else
                {
                    eventData = JsonConvert.DeserializeObject<T>(e.Data);
                }

                messageHandler(eventData);
            };

            ws.OnClose += (sender, e) => { _openSockets.Remove(ws); };

            ws.OnError += (sender, e) => { _openSockets.Remove(ws); };

            ws.Connect();
            _openSockets.Add(ws);
        }

        /// <summary>
        /// Connects to a UserData Websocket endpoint.
        /// </summary>
        /// <param name="parameters">Paremeters to send to the Websocket.</param>
        /// <param name="accountHandler">Deletage to callback after receive a account info message.</param>
        /// <param name="tradeHandler">Deletage to callback after receive a trade message.</param>
        /// <param name="orderHandler">Deletage to callback after receive a order message.</param>
        public void ConnectToUserDataWebSocket(string parameters, MessageHandler<AccountUpdatedMessage> accountHandler,
            MessageHandler<OrderOrTradeUpdatedMessage> tradeHandler,
            MessageHandler<OrderOrTradeUpdatedMessage> orderHandler)
        {
            var finalEndpoint = _webSocketEndpoint + parameters;

            var ws = new WebSocket(finalEndpoint);

            ws.OnMessage += (sender, e) =>
            {
                var message = JsonConvert.DeserializeObject<WebSocketMessage>(e.Data);

                switch (message.EventType)
                {
                    case "outboundAccountInfo":
                        var accountUpdatedMessage = JsonConvert.DeserializeObject<AccountUpdatedMessage>(e.Data);
                        accountHandler(accountUpdatedMessage);
                        break;

                    case "executionReport":
                        var orderOrTradeUpdatedMessage =
                            JsonConvert.DeserializeObject<OrderOrTradeUpdatedMessage>(e.Data);
                        var isTrade = orderOrTradeUpdatedMessage.ExecutionType == ExecutionType.Trade;
                   
                        if (isTrade)
                        {
                            tradeHandler(orderOrTradeUpdatedMessage);
                        }
                        else
                        {
                            orderHandler(orderOrTradeUpdatedMessage);
                        }
                        break;
                }
            };

            ws.OnClose += (sender, e) => { _openSockets.Remove(ws); };

            ws.OnError += (sender, e) => { _openSockets.Remove(ws); };

            ws.Connect();
            _openSockets.Add(ws);
        }

        private class BinanceErrorPayload
        {
            [JsonProperty("code")]
            public int ErrorCode { get; set; }

            [JsonProperty("msg")]
            public string Message { get; set; }
        }
    }
}