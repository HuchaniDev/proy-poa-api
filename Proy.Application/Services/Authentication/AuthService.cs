using System.Net;
using Proy.Application.Services.Security;
using Proy.Domain.Dtos.Authentication;
using Proy.Domain.JwtService;
using Proy.Domain.Repositories.Authentication;
using Proy.Domain.Responses;

namespace Proy.Infrastructure.JwtService;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasherService _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, PasswordHasherService passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> AuthenticateAsync(LoginDto userAuth )
    {
        var user = await _userRepository.GetByUsername(userAuth.UserName);
        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, userAuth.Password))
        {
            return Result<string>.Failure(new List<string> { "Credenciales inválidas" }, HttpStatusCode.Unauthorized);
        }

        
        //var roles = await _userRepository.GetRolesAsync(user.Id); 
        var token = _tokenService.GenerateToken(user);
        
        return Result<string>.Success(token, HttpStatusCode.OK);
    }
}