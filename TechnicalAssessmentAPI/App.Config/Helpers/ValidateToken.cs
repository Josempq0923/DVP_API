using App.Config.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Config.Helpers
{
    public static class ValidateToken
    {
        public static string ValidateTokenUser (UsersDTO user, IConfiguration _configuration)
        {
            string token = string.Empty;
            JwtSecurityTokenHandler tokenHandler = new();

            if (_configuration != null)
            {
                string key = _configuration["JwtConfiguration:Key"] ?? string.Empty;
                string issuer = _configuration["JwtConfiguration:Issuer"] ?? string.Empty;
                string audience = _configuration["JwtConfiguration:Audience"] ?? string.Empty;

                byte[] keyEncoding = Encoding.ASCII.GetBytes(key);

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, (user.UserName ?? string.Empty))
                        }),

                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer= issuer,
                    Audience= audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyEncoding), SecurityAlgorithms.HmacSha256)
                };

                SecurityToken createToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(createToken);

            }

            return token;
        }
    }
}
