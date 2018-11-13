using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DHLM.DeviceManagement.API.ExceptionHandling
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorWrappingMiddleware> _logger;
        
        public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            Exception exception = null;
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                //_logger.LogError(EventIds.GlobalException, ex, ex.Message);
                exception = ex;
                Console.WriteLine(ex.ToString());
                context.Response.StatusCode = 500;
            }                  

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                var response = new ApiResponse(context.Response.StatusCode, 
                                                exception.ToString());
                var json = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(json);
            }            
        }
    }
}