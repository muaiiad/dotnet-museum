using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Controllers;

public class CategoryController : Controller
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }
    
    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }
}