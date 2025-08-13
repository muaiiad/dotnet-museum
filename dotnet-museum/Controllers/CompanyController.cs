using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers;

public class CompanyController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}