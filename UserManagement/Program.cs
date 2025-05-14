using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ThePizzaShop.Helper;
using UserManagement.Models;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UserManagementDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("myconn")));


builder.Services.AddAuthentication(
      JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsTheSecretKeyofGeneratingTheJwtTokenForSecurityPurpose"))
    };
});

builder.Services.AddAuthorization();

 WebApplication? app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<JwtAuthenticationMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
