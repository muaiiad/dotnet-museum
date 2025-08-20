using dotnet_museum.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers;

public class RegisterController : Controller
{
    private readonly AppDbContext _context;

    public RegisterController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult IsUsernameAvailable(string UserName)
    {
        bool usernameExists = _context.Users.Any(u => u.UserName == UserName);

        if (usernameExists)
        {
            return Json("Username already exists, please try a different username");
        }
        return Json(true);
    }
}