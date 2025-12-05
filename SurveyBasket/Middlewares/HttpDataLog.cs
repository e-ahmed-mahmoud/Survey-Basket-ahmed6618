namespace SurveyBasket.Middlewares;

public class HttpDataLog
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public HttpDataLog(ILogger<HttpDataLog> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync (HttpContext httpContext)
    {

        _logger.LogInformation("Source {Source} path {path}",httpContext.Connection.RemoteIpAddress?.ToString(), httpContext.Request.Path );

        await _next(httpContext);
    }


}

public static class HttpDataLogExtentions
{
    public static IApplicationBuilder UseHttpDataLog(this IApplicationBuilder app)
    {
        return app.UseMiddleware<HttpDataLog>();
    }
      
}
