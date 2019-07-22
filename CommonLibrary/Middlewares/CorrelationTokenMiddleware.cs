using System;
using System.Threading.Tasks;
using CommonLibrary.Helpers;
using Microsoft.AspNetCore.Http;

namespace CommonLibrary.Middlewares
{
    public class CorrelationTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationTokenMiddleware(RequestDelegate next) =>
            _next = next ?? throw new ArgumentNullException(nameof(next));

        public Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Correlation-Token"];
            CorrelationContext.SetCorrelationToken(token);
            return _next(context);
        }
    }
}