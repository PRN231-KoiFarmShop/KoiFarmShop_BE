using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ks.domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ks.application.Utilities;
public static class TokenGenerator
{
    public static string GenerateToken(User user,
        string role) 
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("VERYSTRONGPASSWORD_CHANGEMEIFYOUNEED");
        var claimsList = new List<Claim>()
            {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.HomePhone, user.PhoneNumber ?? string.Empty),
            new(ClaimTypes.Role, role)
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = "ks.client",
            Issuer = "ks.api",
            Subject = new ClaimsIdentity(claimsList),
            Expires = DateTime.UtcNow.AddDays(1000),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}