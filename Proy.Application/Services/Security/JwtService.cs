// using System.Security.Claims;
// using System.Text;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
//
// namespace Proy.Application.Services.Security;
//
// public class JwtService
// {
//     private readonly IConfiguration _configuration;
//     
//     public JwtService(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }
//
//     public string GenerateToken(int userId, string userName)
//     {
//         var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]);
//         var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
//         
//         var claims = new[]
//         {
//             new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
//         }
//     }
// }