namespace ks.webapi.Middlewares;
public class GlobalErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        return context.Response.WriteAsync(exception.Message);

    }
}