using Microsoft.AspNetCore.Mvc;
using UserManagement.Helper;
using UserManagement.Models;
using UserManagement.Models.ViewModels;

namespace UserManagement.Controllers;

public class AuthController : Controller
{

    private readonly UserManagementDbContext _context;

    public AuthController(UserManagementDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        string? authToken = Request.Cookies["AuthToken"];

        if (!string.IsNullOrEmpty(authToken))
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }


    [HttpPost]
    public IActionResult Index(LoginPersonViewModel loginPersonViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Persons? person = _context.Persons.FirstOrDefault(u => u.Email == loginPersonViewModel.Email);

        if (person == null)
        {
            TempData["error"] = "Person is Not Found.";
            return View();
        }

        string? token = Token.GenerateJwtToken(person.Id.ToString(), person.Username, person.Role);

        CookieOptions? cookieOptions = new CookieOptions
        {
            Expires =loginPersonViewModel.RememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(24),
            HttpOnly = true,
            IsEssential = true
        };

        Response.Cookies.Append("AuthToken", token, cookieOptions);


        if (person.Password != loginPersonViewModel.Password)
        {
            TempData["error"] = "Invalid Credetials";
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

}
