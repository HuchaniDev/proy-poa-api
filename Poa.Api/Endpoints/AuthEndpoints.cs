using Proy.Domain.Dtos.Authentication;
using Proy.Infrastructure.JwtService;

namespace Poa.Api.Endpoints;

public static class AuthEndpoints
{
    internal static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGroup("/auth").WithTags("Auth").MapGroupAuth();
    }

    internal static void MapGroupAuth(this RouteGroupBuilder routes)
    {
        routes.MapPost(
            "/login",
            async (LoginDto dto, AuthService authService) =>
            {
                var result = await authService.AuthenticateAsync(dto);
                return Results.Json(result, statusCode: (int)result.StatusCode);
            }
        ).AllowAnonymous();

        // routes.MapPost(
        //     "/register",
        //     (RegisterDto dto, AuthService authService) =>
        //     {
        //         var result = authService.Register(dto).Result;
        //         return Results.Json(result, statusCode: (int)result.StatusCode);
        //     }
        // );
        //
        // routes.MapPost(
        //     "/refresh-token",
        //     (RefreshTokenDto dto, AuthService authService) =>
        //     {
        //         var result = authService.RefreshToken(dto).Result;
        //         return Results.Json(result, statusCode: (int)result.StatusCode);
        //     }
        // );
        //
        // routes.MapPost(
        //     "/logout",
        //     (LogoutDto dto, AuthService authService) =>
        //     {
        //         var result = authService.Logout(dto).Result;
        //         return Results.Json(result, statusCode: (int)result.StatusCode);
        //     }
        // );
    }
}