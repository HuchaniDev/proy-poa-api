using Proy.Application.Services;
using Proy.Domain.Dtos.StrategicAxis;

namespace Poa.Api.Endpoints;

public static class StrategicLineEndpoints
{
    internal static void MapStrategicLineEndpoints(this WebApplication WebApp)
    {
        WebApp.MapGroup("/strategic-line").WithTags("Líneas - estrategicas").MapGroupStrategicLine();
    }

    internal static void MapGroupStrategicLine(this RouteGroupBuilder routes)
    {
        routes.MapGet(
            "/strategic-axis-id/{AxisId:int}",
            async (int AxisId, StrategicLineService strategicLineService) =>
            {
                var result = await strategicLineService.GetAllByStrategicAxisId(AxisId);
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
        
        // routes.MapGet(
        //     "/by-description/{text}",
        //     (string text, StrategicLineService strategicLineService) =>
        //     {
        //         var result = strategicLineService.GetByDescription(text).Result;
        //         return Results.Json(result, statusCode: (int)result.StatusCode);
        //     }
        // );
        
        routes.MapPost(
            "/",
            ( StrategicLineRequestDto dto, StrategicLineService strategicLineService) =>
            {
                var result = strategicLineService.Save(dto).Result;
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