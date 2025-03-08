using Proy.Application.Services.Authentication;
using Proy.Domain.Models.Authentication;

namespace Poa.Api.Endpoints;

public static class UserEndpoints
{
    internal static void MapUserEndpoints(this WebApplication WebApp)
    {
        WebApp.MapGroup("/users").WithTags("Usuarios").MapGroupUsers();
    }
    
    internal static void MapGroupUsers(this RouteGroupBuilder app)
    {
        // app.MapGet("/", async (UserService userService) =>
        // {
        //     var users = await userService.GetUsersAsync();
        //     return Results.Ok(users);
        // });
        //
        // app.MapGet("/{id}", async (UserService userService, int id) =>
        // {
        //     var user = await userService.GetUserAsync(id);
        //     return user is null ? Results.NotFound() : Results.Ok(user);
        // });
        //
        app.MapPost("/", async (UserService userService, UserModel user) =>
        {
            var result = await userService.SaveUserAsync(user);
            return Results.Json(result,statusCode:(int)result.StatusCode);
        });
        //
        // app.MapPut("/{id}", async (UserService userService, int id, User user) =>
        // {
        //     var updated = await userService.UpdateUserAsync(id, user);
        //     return updated ? Results.NoContent() : Results.NotFound();
        // });
        //
        // app.MapDelete("/{id}", async (UserService userService, int id) =>
        // {
        //     var deleted = await userService.DeleteUserAsync(id);
        //     return deleted ? Results.NoContent() : Results.NotFound();
        // });
    }
}