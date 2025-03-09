namespace Proy.Domain.Dtos.Authentication;

public record UserRolesPermission(
    int Id,
    string UserName,
    string PasswordHash,
    List<string> Roles,
    List<string> Permissions
    );