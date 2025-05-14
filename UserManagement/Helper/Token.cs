using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserManagement.Helper;

public class Token
{
    public static string GenerateJwtToken(string userId, string userName, string Role)
    {

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, Role),
        };

        var TokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("ThisIsTheSecretKeyofGeneratingTheJwtTokenForSecurityPurpose");

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


