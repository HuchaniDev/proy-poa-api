using Microsoft.AspNetCore.Mvc;
using Proy.Application.Services.Authentication;
using Proy.Domain.Models.Authentication;
using Poa.Api.Middleware;

namespace Poa.Api.Endpoints;

public static class UserEndpoints
{
    internal static void MapUserEndpoints(this WebApplication WebApp)
    {
        WebApp.MapGroup("/users").WithTags("Usuarios").MapGroupUsers();
    }
    
    internal static void MapGroupUsers(this RouteGroupBuilder app)
    {
        app.MapGet("/", async (UserService userService) =>
            {
                var result = await userService.GetAllAsync();
                return Results.Json(result, statusCode: (int)result.StatusCode);
            })
            .RequireAuthorization()
            .RequireRoles("Admin")
            .RequirePermissions("read");

        
        app.MapPost("/", async (UserService userService, UserModel user) =>
        {
            var result = await userService.SaveUserAsync(user);
            return Results.Json(result,statusCode:(int)result.StatusCode);
        })
        .AllowAnonymous();
        
        app.MapPut("change-status/{id}", async ([FromBody]bool status, UserService userService, int id ) =>
        {
            var result = await userService.ChangeStatusActive(id, status);
            return Results.Json(result,statusCode:(int)result.StatusCode);
        });
        
        app.MapPut("change-password/{id}", async ([FromBody]string password, UserService userService, int id ) =>
        {
            var result = await userService.ChangePassword(id, password);
            return Results.Json(result,statusCode:(int)result.StatusCode);
        });
        
        app.MapDelete("/{id}", async (UserService userService, int id) =>
        {
            var result = await userService.DeleteAsync(id);
            return Results.Json(result,statusCode:(int)result.StatusCode);
        });
    }
}