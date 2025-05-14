using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers;

public class ErrorController:Controller
{
    public IActionResult Index(){
        return View();
    }
}
