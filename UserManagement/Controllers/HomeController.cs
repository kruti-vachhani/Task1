
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers;

[Authorize]
public class HomeController : Controller
{

    public IActionResult Index()
    {
        string? role = HttpContext.Items["Role"]?.ToString();
        Console.WriteLine("Role....." + role);
        string? token = Request.Cookies["AuthToken"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index", "Auth");
        }

        return View();
    }

    public IActionResult Logout()
    {
        int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        Console.WriteLine("Id............." + userId);

        Response.Cookies.Delete("AuthToken");
        TempData["success"] = "Logout successful!";

        return RedirectToAction("Index", "Auth");
    }

    public IActionResult Privacy()
    {
        return View();
    }


}
