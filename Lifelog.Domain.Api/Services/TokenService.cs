using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lifelog.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Lifelog.Domain.Api.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // token generator
        var key = Encoding.ASCII.GetBytes(Configuration.Token.JwtKey); // get jwt key to byte[]
        var claim = new Claim(ClaimTypes.Name, user.Email);
        var tokenDescriptior = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        }; // token details
        var token = tokenHandler.CreateToken(tokenDescriptior); // token creation
        return tokenHandler.WriteToken(token); // return string
    }
}