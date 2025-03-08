using System.Net;
using System.Text.Json;
using Proy.Domain.Responses;

namespace Poa.Api.Middleware;

public class NotFoundMiddleware
{
    private readonly RequestDelegate _next;

    public NotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound && !context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";

            var result = Result<object>.Failure(
                new List<string> { "La ruta solicitada no fue encontrada." },
                HttpStatusCode.NotFound);

            var jsonResponse = JsonSerializer.Serialize(result);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}