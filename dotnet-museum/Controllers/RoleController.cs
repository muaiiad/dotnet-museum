using dotnet_museum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace dotnet_museum.Controllers;

public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        this.roleManager = roleManager;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleViewModel newRole)
    {
        if (ModelState.IsValid)
        {
            IdentityRole role = new IdentityRole();
            role.Name = newRole.Name;
            IdentityResult result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View("Create", newRole);
    }
}