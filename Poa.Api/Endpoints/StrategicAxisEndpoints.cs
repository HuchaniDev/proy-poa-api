using System.Security.Cryptography;
using Proy.Application.Services;
using Proy.Domain.Models.StrategicAxis;

namespace Poa.Api.Endpoints;

public static class StrategicAxisEndpoints
{
    internal static void MapStrategicAxisEndpoints(this WebApplication WebApp)
    {
        WebApp.MapGroup("/strategic-axis").WithTags("POA - Ejes").MapGroupStrategicAxis();
    }
    
    internal static void MapGroupStrategicAxis(this RouteGroupBuilder routes)
    {
        routes.MapGet(
            "/",
            (StrategicAxisService strategicAxisService) =>
            {
                var result = strategicAxisService.GetAll().Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
        
        routes.MapGet(
            "/{id}",
            (int id, StrategicAxisService strategicAxisService) =>
            {
                var result = strategicAxisService.GetById(id).Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );

        routes.MapPost(
            "/",
            (StrategicAxisModel model, StrategicAxisService strategicAxisService) =>
            {
                var result = strategicAxisService.Save(model).Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
        
        routes.MapDelete(
            "/{id}",
            (int id, StrategicAxisService strategicAxisService) =>
            {
                var result = strategicAxisService.Delete(id).Result;
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        );
    }
}