using System;
using System.Threading;

namespace CommonLibrary.Helpers
{
    public static class CorrelationContext
    {
        private static readonly AsyncLocal<string> CorrelationToken =
            new AsyncLocal<string>();

        public static void SetCorrelationToken(string correlationToken)
        {
            if (string.IsNullOrWhiteSpace(correlationToken))
                correlationToken = Guid
                    .NewGuid()
                    .ToString();

            CorrelationToken.Value = correlationToken;
        }

        public static string Get() => CorrelationToken.Value;
    }
}