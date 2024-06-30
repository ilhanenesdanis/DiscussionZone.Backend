namespace DiscussionZone.API.Middleware
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeyAuthenticationMiddleware> _logger;
        private readonly IConfiguration _configuration;
        public ApiKeyAuthenticationMiddleware(ILogger<ApiKeyAuthenticationMiddleware> logger, RequestDelegate next, IConfiguration configuration)
        {
            _logger = logger;
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("APIKEY", out var appApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Api Key Not Found");
                _logger.LogCritical("Api Key Not Found");
                return;
            }

            var apiKey = _configuration.GetValue<string>("ApiKey") ?? string.Empty;

            if (!apiKey.Equals(appApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client");
                _logger.LogCritical("Unauthorized client");
                return;
            }
            await _next(context);

        }

    }
}
