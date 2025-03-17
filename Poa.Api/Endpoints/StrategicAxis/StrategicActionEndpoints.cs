using Proy.Application.Services;
using Proy.Domain.Dtos.StrategicAxis;

namespace Poa.Api.Endpoints;

public static class StrategicActionEndpoints
{
    internal static void MapStrategicActionEndpoints(this WebApplication WebApp)
    {
        WebApp.MapGroup("/strategic-action").WithTags("Acciones - estrategicas").MapGroupStrategicAction();
    }
    
    internal static void MapGroupStrategicAction(this RouteGroupBuilder routes)
    {
        routes.MapGet(
            "/strategic-line-id/{LineId:int}",
            async (int LineId, StrategicActionService strategicActionService) =>
            {
                var result = await strategicActionService.GetAllByStrategicLineId(LineId);
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
        
        routes.MapGet(
            "/{id}",
            async (int id, StrategicActionService strategicActionService) =>
            {
                var result = await strategicActionService.GetById(id);
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
        
        // routes.MapGet(
        //     "/by-description/{text}",
        //     (string text, StrategicActionService strategicActionService) =>
        //     {
        //         var result = strategicActionService.GetByDescription(text).Result;
        //         return Results.Json(result, statusCode: (int)result.StatusCode);
        //     }
        // );
        
        routes.MapPost(
            "/",
            async ( StrategicActionRequestDto dto, StrategicActionService strategicActionService) =>
            {
                var result =await strategicActionService.Save(dto);
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
        
        routes.MapDelete(
            "/{id}",
            async (int id, StrategicActionService strategicActionService) =>
            {
                var result = await strategicActionService.Delete(id);
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
    }
}