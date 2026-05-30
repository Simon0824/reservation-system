using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using reservation_system.Models;

namespace reservation_system.Services
{
    public interface ITokenService
    {
        string CreateToken(UserAppModel user);
    }

    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public string CreateToken(UserAppModel user)
        {
            var claims = new Dictionary<string, object>
                    {
                       { JwtRegisteredClaimNames.Sub, user.Id },
                       { JwtRegisteredClaimNames.UniqueName, user.UserName ?? "" },
                       { JwtRegisteredClaimNames.Email, user.Email ?? "" },
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("CreatingToken:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["CreatingToken:Issuer"],
                Audience = configuration["CreatingToken:Audience"],
                Claims = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }
    }
}
