using Proy.Domain.Dtos.Authentication;
using Proy.Domain.Models.Authentication;

namespace Proy.Domain.JwtService;

public interface ITokenService
{
    string GenerateToken(UserRolesPermission userRolesPermission);
}