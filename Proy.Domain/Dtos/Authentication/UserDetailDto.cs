namespace Proy.Domain.Dtos.Authentication;

public record UserDetailDto(
    int Id,
    string UserName,
    string Email,
    bool IsActive,
    DateTime CreatedAt
    );