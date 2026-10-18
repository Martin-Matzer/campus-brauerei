namespace BrauereiApi.API_Key_Implementation
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeader = "X-API-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var key))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API-Key fehlt");
                return;
            }

            var validKey = config["Brauerei:ApiKey"];
            if (key != validKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API-Key ungültig");
                return;
            }

            await _next(context);
        }
    }
}