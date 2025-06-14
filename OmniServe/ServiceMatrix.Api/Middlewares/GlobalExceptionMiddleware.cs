namespace ServiceMatrix.Api.Middlewares;
public class GlobalExceptionMiddleware: IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        ILogger<GlobalExceptionMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server error has occured"
            };

            string json = System.Text.Json.JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
