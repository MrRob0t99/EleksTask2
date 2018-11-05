using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EleksTask
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    context.Response.Clear();
                    await context.Response.WriteAsync(ex.Message);
                    return;
                }
            }
        }
    }
}
