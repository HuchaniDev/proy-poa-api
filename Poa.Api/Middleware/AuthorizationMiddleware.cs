using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Proy.Domain.Repositories.Authentication;
using Proy.Domain.Responses;

namespace Poa.Api.Middleware;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AuthorizationMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        // 🔹 Si el endpoint permite acceso anónimo, no aplicar autorización
        if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
        {
            await _next(context);
            return;
        }

        // 🔹 Obtener el usuario autenticado
        var username = context.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
        {
            await RespondWithUnauthorized(context, "Unauthorized: Missing or invalid token.");
            return;
        }

        // 🔹 Obtener usuario, roles y permisos desde la base de datos
        using var scope = _serviceScopeFactory.CreateScope();
        var _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var userRolesPermissions = await _userRepository.GetPermissionsByUsername(username);

        if (userRolesPermissions == null)
        {
            await RespondWithUnauthorized(context, "Unauthorized: User not found.");
            return;
        }

        // 🔹 Obtener roles y permisos requeridos para el endpoint
        var requiredRoles = context.GetRequiredRoles();
        var requiredPermissions = context.GetRequiredPermissions();

        // 🔹 Convertir listas en HashSet para validación rápida
        var userRoles = new HashSet<string>(userRolesPermissions.Roles);
        var userPermissions = new HashSet<string>(userRolesPermissions.Permissions);

        // 🔹 Validar roles (si hay roles requeridos)
        if (requiredRoles.Any() && !requiredRoles.Any(userRoles.Contains))
        {
            await RespondWithForbidden(context, "Insufficient roles.");
            return;
        }

        // 🔹 Validar permisos (si hay permisos requeridos)
        if (requiredPermissions.Any() && !requiredPermissions.All(userPermissions.Contains))
        {
            await RespondWithForbidden(context, "Insufficient permissions.");
            return;
        }

        // 🔹 Continuar con la solicitud si pasa la validación
        await _next(context);
    }

    private static async Task RespondWithUnauthorized(HttpContext context, string message)
    {
        var failureResult = Result<string>.Failure(new List<string> { message }, HttpStatusCode.Unauthorized);
        context.Response.StatusCode = (int)failureResult.StatusCode;
        await context.Response.WriteAsJsonAsync(failureResult);
    }

    private static async Task RespondWithForbidden(HttpContext context, string message)
    {
        var failureResult = Result<string>.Failure(new List<string> { message }, HttpStatusCode.Forbidden);
        context.Response.StatusCode = (int)failureResult.StatusCode;
        await context.Response.WriteAsJsonAsync(failureResult);
    }
}
