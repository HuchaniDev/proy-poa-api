namespace Proy.Domain.Dtos.Authentication;

public record LoginDto(
    string UserName,
    string Password
    );