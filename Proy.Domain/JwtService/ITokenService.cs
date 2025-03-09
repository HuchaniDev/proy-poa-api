using Proy.Domain.Models.Authentication;

namespace Proy.Domain.JwtService;

public interface ITokenService
{
    string GenerateToken(UserModel user,IList<string> roles);
}