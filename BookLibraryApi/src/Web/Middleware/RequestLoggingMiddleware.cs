namespace BookLibraryApi.src.Web.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next; // Ссылка на следующий метод в пайплайне
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var start = DateTime.UtcNow;
        _logger.LogInformation("--> {Method} {Path} started", context.Request.Method, context.Request.Path);

        await _next(context);

        var elapsed = DateTime.UtcNow - start;
        _logger.LogInformation("<-- {Method} {Path} responded {StatusCode} in {Elapsed}ms", 
            context.Request.Method, 
            context.Request.Path, 
            context.Response.StatusCode, 
            elapsed.TotalMilliseconds);
    }
}
