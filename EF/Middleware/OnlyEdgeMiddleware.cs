using System.Net.Http;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace FrontendBlazor
{
    public class OnlyEdgeMiddleware
    {
        private readonly RequestDelegate _next;
        public OnlyEdgeMiddleware(RequestDelegate next) 
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.UserAgent.Contains("Edg"))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Поддерживается только браузер Edge");
                return;
            }
            await _next(context);
        }
    }
}
