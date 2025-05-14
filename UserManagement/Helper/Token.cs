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
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken? Token = TokenHandler.CreateToken(TokenDescriptor);
        return TokenHandler.WriteToken(Token);
    }


    public static object DecodeToken(string tokenValue)
    {
        JwtSecurityTokenHandler? TokenHandler = new JwtSecurityTokenHandler();
        byte[]? key = Encoding.ASCII.GetBytes("ThisIsTheSecretKeyofGeneratingTheJwtTokenForSecurityPurpose");

        ClaimsPrincipal claimsPrincipal = TokenHandler.ValidateToken(tokenValue, new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        }, out SecurityToken validatedToken);

        JwtSecurityToken? jwttoken = (JwtSecurityToken)validatedToken;
        string? id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string? userName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
        string? role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

        Object decodeToken = new
        {
            Id = id,
            Username = userName,
            Role = role
        };

        return decodeToken;
    }

}


