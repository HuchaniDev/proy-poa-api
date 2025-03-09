using Proy.Domain.Dtos.Authentication;
using Proy.Domain.Responses;

namespace Proy.Domain.JwtService;

public interface IAuthService
{
    Task<Result<string>> AuthenticateAsync(LoginDto loginDto);
}