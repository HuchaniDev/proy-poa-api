using System.Net;
using System.Text.Json;
using Proy.Domain.Responses;

namespace Poa.Api.Middleware;

public class MiddlewareException
{
    private readonly RequestDelegate _next;

    public MiddlewareException(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        
        var result = CreateErrorResponse(exception);

        var jsonResponse = JsonSerializer.Serialize(result);
        return context.Response.WriteAsync(jsonResponse);
    }

    private Result<object> CreateErrorResponse(Exception exception)
    {
        var errors = new List<string>();
        CollectErrorMessages(exception, errors);

        return Result<object>.Failure(errors, HttpStatusCode.InternalServerError);
    }

    private void CollectErrorMessages(Exception exception, List<string> errorList)
    {
        if (exception == null)
            return;

        if (exception is AggregateException aggregateException)
        {
            foreach (var innerException in aggregateException.InnerExceptions)
            {
                CollectErrorMessages(innerException, errorList);
            }
        }
        else
        {
            errorList.Add(exception.Message);
            CollectErrorMessages(exception.InnerException, errorList);
        }
    }
}