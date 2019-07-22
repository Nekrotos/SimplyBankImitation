using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CommonLibrary.Middlewares
{
    public class InternalServerErrorMiddleware
    {
        private readonly ILogger<InternalServerErrorMiddleware> _logger;
        private readonly RequestDelegate _next;

        public InternalServerErrorMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next ??
                    throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<InternalServerErrorMiddleware>() ??
                      throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogError(e.Message, e.StackTrace);
                    throw;
                }

                _logger.LogError(e.Message, e.StackTrace);
                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = @"application/json";

                var json = JsonConvert.SerializeObject(e.Message);
                await context.Response.WriteAsync(json);
            }
        }
    }
}