using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
                
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                context.User = claimsPrincipal;

                 Claim?  roleClaim = jwtToken.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Role);
                 Claim?  userName = jwtToken.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Name);

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