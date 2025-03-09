using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Proy.Domain.JwtService;
using Proy.Domain.Models.Authentication;
using Proy.Infrastructure.DataBase.EntityFramework.Entities.Authentication;

namespace Proy.Infrastructure.JwtService;

public class TokenService:ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly string _jwtSecret;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSecret = _configuration["JwtSettings:SecretKey"];
    }

    public string GenerateToken(UserModel user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}