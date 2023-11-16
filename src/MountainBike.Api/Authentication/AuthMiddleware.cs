namespace MountainBike.Api.Authentication;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthMiddleware> _logger;

    public AuthMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<AuthMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key missing");
            return;
        }

        var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

        if (apiKey is null)
        {
            _logger.LogError("API Key is not configured");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Error");
            return;
        }

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key invalid");
            return;
        }

        await _next(context);
    }
}