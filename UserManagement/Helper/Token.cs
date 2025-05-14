using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserManagement.Helper;

public class Token
{
    public static string GenerateJwtToken(string userId, string userName, string Role)
    {
        Claim[]? claims = new[]
       {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, Role),
        };

        JwtSecurityTokenHandler? TokenHandler = new JwtSecurityTokenHandler();
        byte[]? key = Encoding.ASCII.GetBytes("ThisIsTheSecretKeyofGeneratingTheJwtTokenForSecurityPurpose");

        SecurityTokenDescriptor? TokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken? Token = TokenHandler.CreateJwtSecurityToken(TokenDescriptor);
        return TokenHandler.WriteToken(Token);
    }
}


