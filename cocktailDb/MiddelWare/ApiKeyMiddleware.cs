namespace CocktailDb.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEY = "Api-Key";
    //get key from appsettings.json
    private readonly List<string> _allowedApiKeys;

    private readonly ILogger<ApiKeyMiddleware> _logger;
    public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger, IOptions<ApiKeySettings> apiKeySettings)
    {
        _next = next;
        _logger = logger;
        _allowedApiKeys = apiKeySettings.Value.APIKey;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key was not provided");
            LogUnauthorizedAttempt(extractedApiKey);
            return;
        }

        if (_allowedApiKeys.Contains(extractedApiKey))
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client");
            LogUnauthorizedAttempt(extractedApiKey);
        }
    }

    private void LogUnauthorizedAttempt(string apiKey)
    {
        var currentTime = DateTime.Now;
        _logger.LogInformation($"Unauthorized access attempt at {currentTime} with Key {apiKey}");
    }
    //for one key
    //var apiKey = appSettings.Value.APIKey;
    // if (!apiKey.Equals(extractedApiKey))
    // {
    //     context.Response.StatusCode = 401;
    //     await context.Response.WriteAsync("Unauthorized client");
    //     return;
    // }
    // await _next(context);
}


