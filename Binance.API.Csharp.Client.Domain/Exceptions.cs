using System;

namespace Binance.API.Csharp.Client.Domain
{
    public class BinanceApiException : Exception
    {
        public BinanceApiException()
        {
        }

        public BinanceApiException(string message) : base(message)
        {
        }

        public BinanceApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class InvalidRequestException : BinanceApiException
    {
        public InvalidRequestException()
        {
        }

        public InvalidRequestException(int errorCode, string message) : base(message) => ErrorCode = errorCode;

        public InvalidRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public int ErrorCode { get; }
    }
}