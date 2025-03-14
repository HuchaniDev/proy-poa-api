using Proy.Application.Services;

namespace Poa.Api.Endpoints;

public static class StrategicLineEndpoints
{
    internal static void MapStrategicLineEndpoints(this WebApplication WebApp)
    {
        WebApp.MapGroup("/strategic-line").WithTags("POA - Líneas").MapGroupStrategicLine();
    }

    internal static void MapGroupStrategicLine(this RouteGroupBuilder routes)
    {
        routes.MapGet(
            "/",
            (StrategicLineService strategicLineService) =>
            {
                var result = strategicLineService.GetAll().Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
        
        routes.MapGet(
            "/{id}",
            (int id, StrategicLineService strategicLineService) =>
            {
                var result = strategicLineService.GetById(id).Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );

        routes.MapGet(
            "/by-description/{text}",
            (string text, StrategicLineService strategicLineService) =>
            {
                var result = strategicLineService.GetByDescription(text).Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );

        routes.MapPost(
            "/",
            (StrategicLineModel model, StrategicLineService strategicLineService) =>
            {
                var result = strategicLineService.Save(model).Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
        
        routes.MapDelete(
            "/{id}",
            (int id, StrategicLineService strategicLineService) =>
            {
                var result = strategicLineService.Delete(id).Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
    }
}