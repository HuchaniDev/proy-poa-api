namespace Poa.Api.Middleware;

public static class AuthorizationExtensions
{
    // Este método de extensión ahora se aplica a RouteHandlerBuilder
    public static RouteHandlerBuilder RequireRoles(this RouteHandlerBuilder builder, params string[] roles)
    {
        builder.Add(endpoint => endpoint.Metadata.Add(new RolesMetadata(roles)));
        return builder;
    }

    public static RouteHandlerBuilder RequirePermissions(this RouteHandlerBuilder builder, params string[] permissions)
    {
        builder.Add(endpoint => endpoint.Metadata.Add(new PermissionsMetadata(permissions)));
        return builder;
    }

    public static string[] GetRequiredRoles(this HttpContext context)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<RolesMetadata>() is RolesMetadata rolesMetadata)
        {
            return rolesMetadata.Roles;
        }
        return Array.Empty<string>();
    }

    public static string[] GetRequiredPermissions(this HttpContext context)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<PermissionsMetadata>() is PermissionsMetadata permissionsMetadata)
        {
            return permissionsMetadata.Permissions;
        }
        return Array.Empty<string>();
    }
}

public class RolesMetadata
{
    public string[] Roles { get; }

    public RolesMetadata(string[] roles)
    {
        Roles = roles;
    }
}

public class PermissionsMetadata
{
    public string[] Permissions { get; }

    public PermissionsMetadata(string[] permissions)
    {
        Permissions = permissions;
    }
}
