using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ThePizzaShop.Helper;

public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? token = context.Request.Cookies["AuthToken"];

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var key = Encoding.UTF8.GetBytes("ThisIsTheSecretKeyofGeneratingTheJwtTokenForSecurityPurpose");

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                var claimsPrincipal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true
                }, out SecurityToken validatedToken);

                context.User = claimsPrincipal;

                Claim? roleClaim = claimsPrincipal.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Role);
                Claim? userName = claimsPrincipal.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Name);

                if (roleClaim != null)
                {
                    context.Items["Role"] = roleClaim.Value;
                    context.Items["UserName"] = userName!.Value;
                }

            }
            catch (Exception)
            {
                return;
            }
        }
        await _next(context);
    }

}