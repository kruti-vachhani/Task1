using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers;

[Authorize(Roles = "User")]
public class HomeController : Controller
{

    public IActionResult Index()
    {
        string? role = HttpContext.Items["Role"]?.ToString();
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
        Response.Cookies.Delete("AuthToken");
        TempData["success"] = "Logout successful!";

        return RedirectToAction("Index", "Auth");
    }

}
