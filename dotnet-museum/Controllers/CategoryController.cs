using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_museum.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _repo;

    public CategoryController(ICategoryRepository repo)
    {
        _repo = repo;
    }
    
    public IActionResult GetAllData()
    {
        return Ok(_repo.GetCategories());
    }
    public IActionResult GetById(int id)
    {
        return Ok(_repo.GetById(id));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _repo.CreateCategory(category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }
    
    public IActionResult Index()
    {
        var categories = _repo.ListCategories();
        
        return View(categories);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var category = _repo.GetById(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _repo.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }
}